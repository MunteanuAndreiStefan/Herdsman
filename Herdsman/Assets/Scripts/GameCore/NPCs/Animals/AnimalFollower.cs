using GameCore.Animal.Interfaces;
using UnityEngine;
using Utils;

namespace GameCore.Animal
{
    [RequireComponent(typeof(Collider2D))]
    public class AnimalFollower : MonoBehaviour, IAnimalBehavior
    {
        public Transform Target;
        public float FollowDistance = Constants.DEFAULT_FOLLOW_DISTANCE;
        public float MoveSpeed = Constants.DEFAULT_ANIMAL_MOVE_SPEED;
        private IAnimalObserver _observer;
        public Collider2D Collider2D { get; private set; }

        protected virtual void Awake() => Collider2D = GetComponent<Collider2D>();

        private void Update() => UpdateBehavior();

        public virtual void SetTarget(Transform newTarget, IAnimalObserver observer)
        {
            Target = newTarget;
            _observer = observer;
        }

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