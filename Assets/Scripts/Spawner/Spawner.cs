using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ObjectPooler pool;
    [SerializeField] private float moveInterval = 3f;
    [SerializeField] private float spawnDistanceFromPlayer = 5f;

    private float _moveTimer;
    private Transform _playerTransform;

    void Awake()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            _playerTransform = player.transform;
        }
    }

    void Start()
    {
        _moveTimer = moveInterval;
        RandomMove();
    }

    void Update()
    {
        if (pool == null)
        {
            return;
        }

        if (_playerTransform == null)
        {
            Player player = FindObjectOfType<Player>();
            if (player == null)
            {
                return;
            }

            _playerTransform = player.transform;
        }

        _moveTimer -= Time.deltaTime;
        if (_moveTimer > 0f)
        {
            return;
        }

        _moveTimer = moveInterval;
        RandomMove();
        SpawnEnemy();
    }

    void RandomMove()
    {
        if (_playerTransform == null)
        {
            return;
        }

        Vector2 randomDirection = Random.insideUnitCircle;
        if (randomDirection.sqrMagnitude <= 0.001f)
        {
            randomDirection = Vector2.right;
        }

        randomDirection.Normalize();
        Vector3 spawnOffset = new Vector3(randomDirection.x, randomDirection.y, 0f) * spawnDistanceFromPlayer;
        Vector3 nextPosition = _playerTransform.position + spawnOffset;
        nextPosition.z = 0f;
        transform.position = nextPosition;
    }

    void SpawnEnemy()
    {
        if (pool == null)
        {
            return;
        }

        GameObject enemy = pool.GetPooledObject();
        if (enemy == null)
        {
            return;
        }

        enemy.transform.position = transform.position;
        enemy.transform.rotation = Quaternion.identity;
        enemy.SetActive(true);
    }
}
