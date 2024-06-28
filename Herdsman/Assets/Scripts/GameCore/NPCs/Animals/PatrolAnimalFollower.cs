using System.Collections.Generic;
using GameCore.Animal.Interfaces;
using GameCore.Spawn;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;
using UnityEngine.AI;
using NavMesh = Utils.NavMesh;

namespace GameCore.Animal
{
    namespace GameLogic.NPCs.Animals
    {
        /// <summary>
        /// AnimalFollower logic with NavMesh agent in order to limit the movement of the animal.
        /// </summary>
        [RequireComponent(typeof(NavMeshAgent))]
        public class PatrolAnimalFollower : AnimalFollower
        {
            public List<Vector3> PatrolPoints;
            [SerializeField] private NavMeshAgent _agent;
            private int _currentPatrolIndex = 0;
            private ISpawnStrategy _spawnStrategy;

            protected override void Awake()
            {
                base.Awake();
                _agent.updateRotation = false;
                _agent.updateUpAxis = false;
                _spawnStrategy = DiContainer.Instance.ServiceProvider.GetRequiredService<ISpawnStrategy>();
                PatrolPoints = new List<Vector3>
                {
                    _spawnStrategy.GetSpawnPosition(), _spawnStrategy.GetSpawnPosition(),
                    _spawnStrategy.GetSpawnPosition(), _spawnStrategy.GetSpawnPosition()
                };

                var currentPatrol = 0;
                while (currentPatrol < DiContainer.Instance.GameConfig.PatrolPoints)
                {
                    var point = _spawnStrategy.GetSpawnPosition();
                    point = new Vector3(point.x, point.y, 0);
                    if (!NavMesh.IsPointAccessible(point)) continue;

                    PatrolPoints.Add(_spawnStrategy.GetSpawnPosition());
                    currentPatrol++;
                }
            }

            /// <summary>
            /// Sets the target of the animal and disables the NavMesh agent.
            /// </summary>
            /// <param name="newTarget">What to follow</param>
            /// <param name="observer">IAnimalObserver implementation to watch</param>
            public override void SetTarget(Transform newTarget, IAnimalObserver observer)
            {
                base.SetTarget(newTarget, observer);
                _agent.enabled = false;
            }

            /// <summary>
            /// Either follows the target or patrols if there is no target.
            /// </summary>
            public override void UpdateBehavior()
            {
                if (Target == null)
                {
                    Patrol();
                }
                else
                {
                    base.UpdateBehavior();
                }
            }

            private void Patrol()
            {
                if (PatrolPoints.Count == 0) return;

                var patrolPoint = PatrolPoints[_currentPatrolIndex];
                _agent.SetDestination(patrolPoint);
                var direction = patrolPoint - transform.position;
                if (direction.magnitude <= 0.6f)
                    _currentPatrolIndex = (_currentPatrolIndex + 1) % PatrolPoints.Count;
            }
            
            private void OnEnable()
            {
                if (_agent == null)
                    _agent = GetComponent<NavMeshAgent>();
                _agent.enabled = true;
            }
        }
    }
}