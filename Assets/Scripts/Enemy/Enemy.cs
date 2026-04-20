using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData data;
    private CircleCollider2D _enemyCollider;
    private float _currentSpeed;
    private Transform _playerTransform;
    void Awake()
    {
        _enemyCollider = gameObject.GetComponent<CircleCollider2D>();
        _enemyCollider.radius = data.detectionRange;

        _playerTransform = GameObject.Find("Player").GetComponent<Transform>();
    }
    void Start()
    {
        _currentSpeed = data.speed;
    }

    private void Update()
    {
        
    }



    private void RandomMove()
    {
        
    }
}
