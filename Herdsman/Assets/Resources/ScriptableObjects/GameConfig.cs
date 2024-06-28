using UnityEngine;

/// <summary>
/// Game configuration
/// </summary>
[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig")]
public class GameConfig : ScriptableObject
{
    [Header("Player")]
    [SerializeField] private int _groupMaxSize = 5;
    [SerializeField] private float _groupRange = 1.5f;
    [SerializeField] private float _playerMoveSpeed = 5f;
    
    [Header("Animal")]
    [SerializeField] private float _animalMoveSpeed = 3f;

    [SerializeField] private float _animalPatrolSpeed = 2f;
    [SerializeField] private float  _animalSize = 0.75f;

    [SerializeField] private float _minSpawnTime = 1f;
    [SerializeField] private float _maxSpawnTime = 5f;
    
    [Header("Patrol")]
    [SerializeField] private  int _patrolPoints = 8;
    

    [Header("NavMesh")]
    [SerializeField] private float _followDistance = 0.6f;
    
    public int GroupMaxSize => _groupMaxSize;
    public float GroupRange => _groupRange;
    public float PlayerMoveSpeed => _playerMoveSpeed;
    public float AnimalMoveSpeed => _animalMoveSpeed;
    public float FollowDistance => _followDistance;
    public float AnimalPatrolSpeed => _animalPatrolSpeed;
    public float AnimalSize => _animalSize;
    public float MinSpawnTime => _minSpawnTime;
    public float MaxSpawnTime => _maxSpawnTime;
    public int PatrolPoints => _patrolPoints;
}
