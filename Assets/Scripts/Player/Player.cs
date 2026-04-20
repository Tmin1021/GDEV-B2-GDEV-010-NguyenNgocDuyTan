using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static event Action<float> OnHealthChange;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 3f;
    [SerializeField] private float damageCooldown = 2f;

    private float _damageTimer;
    private Vector3 _targetPos;
    private bool _hasTarget;

    void Start()
    {
        _targetPos = transform.position;
        OnHealthChange?.Invoke(health);
    }

    void Update()
    {
        if (_damageTimer > 0f)
        {
            _damageTimer -= Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0) && Camera.main != null)
        {
            _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _targetPos.z = 0;
            _hasTarget = true;
        }

        if (_hasTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPos) <= 0.01f)
            {
                _hasTarget = false;
            }
        }
    }

    public float GetPlayerSpeed()
    {
        return speed;
    }

    public void TryTakeDamage(float damage)
    {
        if (_damageTimer > 0f || health <= 0f)
        {
            return;
        }

        TakeDamage(damage);
        _damageTimer = damageCooldown;
    }

    private void TakeDamage(float damage)
    {
        health = Math.Max(0, health - damage);
        OnHealthChange?.Invoke(health);
    }
}
