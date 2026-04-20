using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyData data;
    [SerializeField] private float chaseSpeedMultiplier = 1.2f;
    [SerializeField] private float attackRange = 0.75f;
    [SerializeField] private float roamRadius = 2f;
    [SerializeField] private float roamRetargetInterval = 2f;

    private Player _player;
    private Vector3 _roamTarget;
    private float _roamTimer;

    void Awake()
    {
        _player = FindObjectOfType<Player>();
    }

    void OnEnable()
    {
        _roamTarget = transform.position;
        _roamTimer = 0f;
    }

    private void Update()
    {
        if (data == null)
        {
            return;
        }

        if (_player == null)
        {
            _player = FindObjectOfType<Player>();
        }

        if (_player == null)
        {
            Roam();
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);
        if (distanceToPlayer <= data.detectionRange)
        {
            ChasePlayer();
            if (distanceToPlayer <= attackRange)
            {
                _player.TryTakeDamage(data.damage);
            }

            return;
        }

        Roam();
    }

    private void ChasePlayer()
    {
        float chaseSpeed = Mathf.Max(data.speed, _player.GetPlayerSpeed() * chaseSpeedMultiplier);
        transform.position = Vector3.MoveTowards(
            transform.position,
            _player.transform.position,
            chaseSpeed * Time.deltaTime
        );
    }

    private void Roam()
    {
        _roamTimer -= Time.deltaTime;

        if (_roamTimer <= 0f || Vector3.Distance(transform.position, _roamTarget) <= 0.1f)
        {
            PickNewRoamTarget();
        }

        transform.position = Vector3.MoveTowards(transform.position, _roamTarget, data.speed * Time.deltaTime);
    }

    private void PickNewRoamTarget()
    {
        Vector2 randomOffset = Random.insideUnitCircle * roamRadius;
        _roamTarget = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0f);
        _roamTimer = roamRetargetInterval;
    }
}
