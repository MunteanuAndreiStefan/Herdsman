using System.Collections.Generic;
using GameCore.Animal;
using GameCore.Animal.GameLogic.NPCs.Animals;
using GameCore.Animal.Interfaces;
using UnityEngine;

namespace GameCore.Player
{
    /// <summary>
    /// Main component for the player character.
    /// </summary>
    public class HeroLogic : PlayerMovement, IAnimalObserver
    {
        [SerializeField] private float _groupRange;
        [SerializeField] private int _maxGroupSize;
        private Dictionary<Collider2D, AnimalFollower> _groupedAnimals = new Dictionary<Collider2D, AnimalFollower>();
        private Collider2D[] _colliderResults;
        private ContactFilter2D _contactFilter = new ContactFilter2D();

        /// <summary>
        /// Removes animal from player group, based on it collider.
        /// </summary>
        /// <param name="collider2d">Collider of the animal following it.</param>
        public void OnAnimalDisabled(Collider2D collider2d) => _groupedAnimals.Remove(collider2d);

        protected override void Update()
        {
            base.Update();
            CheckForNearbyAnimals();
        }

        protected override void Start()
        {
            base.Start();
            _contactFilter.layerMask = LayerMask.GetMask("Animal");
        }

        private void Awake()
        {
            _groupRange = DiContainer.Instance.GameConfig.GroupRange;
            _maxGroupSize = DiContainer.Instance.GameConfig.GroupMaxSize;
            _colliderResults = new Collider2D[DiContainer.Instance.GameConfig.GroupMaxSize];
        }

        private void CheckForNearbyAnimals() // This is not optimal even if is non-alloc, the best way to do this is to use a second child object with a collider
                                             // that checks for triggers, and is deactivated after 5 animals are found.
                                             // The code is just for showcasing some inheritance.
        {
            if (_groupedAnimals.Count >= _maxGroupSize) return;

            var hitCount = Physics2D.OverlapCircle(transform.position, _groupRange, _contactFilter, _colliderResults);
            for (var i = 0; i < hitCount; i++)
            {
                if (_groupedAnimals.ContainsKey(_colliderResults[i])) continue;

                var animal = _colliderResults[i].GetComponent<PatrolAnimalFollower>();
                if (animal == null) continue;

                animal.SetTarget(transform, this);
                _groupedAnimals.Add(_colliderResults[i], animal);

                if (_groupedAnimals.Count >= _maxGroupSize) break;
            }
        }
    }
}