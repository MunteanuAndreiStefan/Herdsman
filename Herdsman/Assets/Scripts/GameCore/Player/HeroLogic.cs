using System.Collections.Generic;
using GameCore.Animal;
using GameCore.Animal.GameLogic.NPCs.Animals;
using GameCore.Animal.Interfaces;
using UnityEngine;
using Utils;

namespace GameCore.Player
{
    public class HeroLogic : PlayerMovement, IAnimalObserver
    {
        [SerializeField] private float _groupRange = Constants.DEFAULT_GROUP_RANGE;
        [SerializeField] private int _maxGroupSize = Constants.DEFAULT_GROUP_SIZE;
        private Dictionary<Collider2D, AnimalFollower> _groupedAnimals = new Dictionary<Collider2D, AnimalFollower>();
        private readonly Collider2D[] _colliderResults = new Collider2D[Constants.DEFAULT_GROUP_SIZE];
        private ContactFilter2D _contactFilter = new ContactFilter2D();

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