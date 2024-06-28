using GameCore.Animal.Interfaces;
using UnityEngine;

namespace GameCore.Animal
{
    /// <summary>
    /// Animal follower behavior for following a target.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class AnimalFollower : MonoBehaviour, IAnimalBehavior
    {
        public Transform Target;
        public float FollowDistance;
        public float MoveSpeed;
        private IAnimalObserver _observer;
        public Collider2D Collider2D { get; private set; }

        protected virtual void Awake()
        {
            Collider2D = GetComponent<Collider2D>();
            FollowDistance = DiContainer.Instance.GameConfig.FollowDistance;
            MoveSpeed = DiContainer.Instance.GameConfig.AnimalMoveSpeed;
        }

        private void Update() => UpdateBehavior();

        /// <summary>
        /// Sets the target for the animal to follow.
        /// </summary>
        /// <param name="newTarget">Transform target</param>
        /// <param name="observer">IAnimalObserver implementation to watch</param>
        public virtual void SetTarget(Transform newTarget, IAnimalObserver observer)
        {
            Target = newTarget;
            _observer = observer;
        }

        /// <summary>
        /// Behavior update for the animal, based on target.
        /// </summary>
        public virtual void UpdateBehavior()
        {
            if (Target == null) return;

            FollowTarget();
        }

        private void FollowTarget()
        {
            var direction = Target.position - transform.position;
            if (!(direction.magnitude > FollowDistance)) return;
            direction.Normalize();
            transform.position += direction * MoveSpeed * Time.deltaTime;
        }

        private void OnEnable() => ClearFollow();

        private void OnDisable() => ClearFollow();

        private void ClearFollow()
        {
            if (_observer != null)
                _observer.OnAnimalDisabled(Collider2D);
            Target = null;
        }
    }
}