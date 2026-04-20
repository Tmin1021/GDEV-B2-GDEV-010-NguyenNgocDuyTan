using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public static event Action<float> OnHealthChange;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float health = 3f;

    // Update is called once per frame
    void Update()
    {
        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            RaycastHit2D raycastHit = Physics2D.Raycast(worldPoint, Vector2.zero, Mathf.Infinity);

            if(raycastHit.collider != null)
            {
                // moving player by clicking mouse
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
