using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyData data;
    private CircleCollider2D _enemyCollider;
    private float _currentSpeed;
    private Transform _playerTransform;
    private bool _ifDetectPlayer = false;
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
        if(_ifDetectPlayer)
        {
            MoveToTarget();
        }
        else
        {
            RandomMove();
        }
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Player player = collision.GetComponent<Player>();
            _currentSpeed = player.GetPlayerSpeed() * 1.2f;
            // MoveToTarget();
            _ifDetectPlayer = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            _currentSpeed = data.speed;
            _ifDetectPlayer = false;
        }
    }

    private void RandomMove()
    {
        
    }

    private void MoveToTarget()
    {
        
    }
}
