using System;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private int _amor;

    private int _healHealth = 10;

    public event Action Died;

    public void TakeDamage(int damage)
    {
        damage -= _amor;

        if (damage <= 0)
        {
            damage = 1;
        }

        _health -= damage;

        if (_health <= 0)
        {
            Died?.Invoke();

            Destroy(gameObject);
        }
    }

    public void Heal()
    {
        int minHealth = 0;
        int maxHealth = 100;

        _health = Mathf.Clamp(_health + _healHealth, minHealth, maxHealth);
    }
}
