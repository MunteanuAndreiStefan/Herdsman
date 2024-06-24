using System.Collections.Generic;
using GameCore.Animal.Interfaces;
using GameCore.Spawn;
using Microsoft.Extensions.DependencyInjection;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using NavMesh = Utils.NavMesh;

namespace GameCore.Animal
{
    namespace GameLogic.NPCs.Animals
    {
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
                while (currentPatrol < Constants.DEFAULT_PATROL_POINTS)
                {
                    var point = _spawnStrategy.GetSpawnPosition();
                    point = new Vector3(point.x, point.y, 0);
                    if (!NavMesh.IsPointAccessible(point)) continue;

                    PatrolPoints.Add(_spawnStrategy.GetSpawnPosition());
                    currentPatrol++;
                }
            }

            private void OnEnable()
            {
                if (_agent == null)
                    _agent = GetComponent<NavMeshAgent>();
                _agent.enabled = true;
            }

            public override void SetTarget(Transform newTarget, IAnimalObserver observer)
            {
                base.SetTarget(newTarget, observer);
                _agent.enabled = false;
            }

            public override void UpdateBehavior()
            {
                if (Target == null)
                {
                    Patrol();
                }
                else
                {
                    base.UpdateBehavior(); // Call the base class Update method for following behavior
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
        }
    }
}