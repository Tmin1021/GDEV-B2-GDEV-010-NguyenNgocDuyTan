using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action<float> OnHealthChange;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 3f;
    private float _escapeInterval = 2f;
    private float _timer;
    private Vector3 _targetPos;

    void Start()
    {
        OnHealthChange?.Invoke(health);
    }

    void Update() {
        _timer -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0)) { 
            _targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _targetPos.z = 0;
        }
        // Move towards the target position over time
        transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            if(_timer <= 0)
            {
                TakeDamage(1f);
                _timer = _escapeInterval;
            }
        }
    }

    public float GetPlayerSpeed()
    {
        return speed;
    }

    private void TakeDamage(float damage)
    {
        health = Math.Max(0, health-damage);
        OnHealthChange?.Invoke(health);
    }
}
