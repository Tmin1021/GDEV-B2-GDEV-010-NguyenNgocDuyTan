using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    [SerializeField] ObjectPooler pool;
    private float _moveInterval = 3f;
    private float _moveTimer;
    private float _distance = 5f;
    private Transform _playerTransform;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        _moveTimer -= _moveInterval;
        if(_moveTimer <= 0)
        {
            RandomMove();
            SpawnEnemy();
        }
    }

    void RandomMove()
    {
        // move (teleport) to a random position with a distance from player

    }

    void SpawnEnemy()
    {
        GameObject enemy = pool.GetPooledObject();
        enemy.transform.position = transform.position;
        enemy.SetActive(true);
    }
}
