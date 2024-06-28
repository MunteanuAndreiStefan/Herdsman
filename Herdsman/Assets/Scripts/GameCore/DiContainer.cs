using System.Linq;
using GameCore.Animal;
using GameCore.Animal.Interfaces;
using GameCore.Player;
using GameCore.Spawn;
using GameInput;
using GameUI;
using Microsoft.Extensions.DependencyInjection;
using GameUI.Interfaces;
using UnityEngine;
using GameInput.Interfaces;
using Utils.Generics;

namespace GameCore
{
    /// <summary>
    /// DiContainer serves as a dependency injection (DI) container for managing and resolving dependencies within the game.
    /// </summary>
    public class DiContainer : PersistentSingleton<DiContainer>
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public Rabbit RabbitPrefab;
        public Sheep SheepPrefab;
        public GameObject PlayerPrefab;
        [SerializeField] private GameObject _staticCanvas;
        [SerializeField] private GameObject _dynamicCanvas;
        [SerializeField] private GameConfig _gameConfig;
        IAnimalSpawner _animalSpawner;

        public GameConfig GameConfig
        {
            get
            {
                if(_gameConfig == null)
                    _gameConfig = Resources.Load<GameConfig>("ScriptableObjects/GameConfig");
                return _gameConfig;
            }
        }

        public override void Awake()
        {
            base.Awake();
            var serviceCollection = new ServiceCollection();

            var rabbitPool = new AnimalObjectPool<Rabbit>(RabbitPrefab, 0); //Default without parent for performance reasons, due dirty flag on hierarchy.
            var sheepPool = new AnimalObjectPool<Sheep>(SheepPrefab, 0);

            #region Register services

            serviceCollection.AddSingleton<IScoreManager, ScoreManager>();
            var animalFactory = new AnimalFactory();
            animalFactory.RegisterAnimal("Rabbit", () => rabbitPool.Get(),
                animal => rabbitPool.ReturnToPool((Rabbit)animal));
            animalFactory.RegisterAnimal("Sheep", () => sheepPool.Get(),
                (animal) => sheepPool.ReturnToPool((Sheep)animal));
            serviceCollection.AddSingleton<IAnimalFactory>(animalFactory);
            serviceCollection.AddScoped<ISpawnStrategy, RandomSpawnStrategy>(); // Default strategy
            serviceCollection.AddSingleton<IAnimalSpawner, AnimalSpawner>();
            serviceCollection.AddSingleton<UIManager>();
            serviceCollection.AddSingleton<IPlayerInitializer>(new PlayerInitializer(PlayerPrefab));

#if UNITY_ANDROID || UNITY_IOS // Assume no touch input on desktop
        serviceCollection.AddSingleton<IInputHandler, TouchInputHandler>();
#else
            serviceCollection.AddSingleton<AbstractInputHandler, MouseInputHandler>();
#endif
            serviceCollection.AddSingleton<IInputManager, InputManager>();

            UIManager.Instance.SetupCanvas(_staticCanvas, _dynamicCanvas);
            serviceCollection.AddSingleton<IUIManager>(UIManager.Instance);
            ServiceProvider = serviceCollection.BuildServiceProvider();

            #endregion Register services

            _animalSpawner = ServiceProvider.GetRequiredService<IAnimalSpawner>();
            var strategy = ServiceProvider.GetRequiredService<ISpawnStrategy>();
            _animalSpawner.SetSpawnStrategy(strategy);
        }

        private void Start()
        {
            ServiceProvider.GetRequiredService<IPlayerInitializer>().InitializePlayer(Vector3.zero);
            StartCoroutine(ServiceProvider.GetRequiredService<IAnimalSpawner>().SpawnRandomAnimalRoutine());
            ServiceProvider.GetRequiredService<IUIManager>().StartGame();
        }

        private void Update()
        {
            foreach (var (material, animals) in _animalSpawner.GetActiveAnimals())
            {
                if (animals.Count <= 0) continue;
                    Graphics.DrawMeshInstanced(animals.First().GetMesh(), 0, material,  animals.Select(a => a.GetCurrentMatrix()).ToList()); // Not optimal due selection and ToList allocation, could be optimized via adding all this in the object pool logic.
            }
        }

        private void OnDestroy() =>
            ServiceProvider?.Dispose(); // Null coalescing operator can be used since is not a Component.
    }
}