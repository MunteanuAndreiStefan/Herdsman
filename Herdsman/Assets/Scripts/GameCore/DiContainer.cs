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
    public class DiContainer : PersistentSingleton<DiContainer>
    {
        public ServiceProvider ServiceProvider { get; private set; }

        public Rabbit RabbitPrefab;
        public Sheep SheepPrefab;
        public GameObject PlayerPrefab;
        [SerializeField] private GameObject _staticCanvas;
        [SerializeField] private GameObject _dynamicCanvas;

        public override void Awake()
        {
            base.Awake();
            var serviceCollection = new ServiceCollection();

            var rabbitPool = new AnimalObjectPool<Rabbit>(RabbitPrefab, 0); //Default without parent for performance reasons
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

            var animalSpawner = ServiceProvider.GetRequiredService<IAnimalSpawner>();
            var strategy = ServiceProvider.GetRequiredService<ISpawnStrategy>();
            animalSpawner.SetSpawnStrategy(strategy);
        }

        private void Start()
        {
            ServiceProvider.GetRequiredService<IPlayerInitializer>().InitializePlayer(Vector3.zero);
            StartCoroutine(ServiceProvider.GetRequiredService<IAnimalSpawner>().SpawnRandomAnimalRoutine());
            ServiceProvider.GetRequiredService<IUIManager>().StartGame();
        }

        void OnDestroy() =>
            ServiceProvider?.Dispose(); // Null coalescing operator can be used since is not a Component.
    }
}