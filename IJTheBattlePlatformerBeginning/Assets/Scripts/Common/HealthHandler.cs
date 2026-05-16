using System;
using UnityEngine;

public class HealthHandler : MonoBehaviour, IDamageable
{
    [SerializeField] private int _health;
    [SerializeField] private int _amor;

    private int _healHealth = 10;
    private  int _minHealth = 0;
    private int _maxHealth = 100;

    public event Action Died;

    public void TakeDamage(int damage)
    {
        damage -= _amor;

        if (damage <= 0)
        {
            damage = 1;
        }

        _health = Mathf.Clamp(_health - damage, _minHealth, _maxHealth);

        CheckHealth();
    }

    public void Heal()
    {
        _health = Mathf.Clamp(_health + _healHealth, _minHealth, _maxHealth);

        CheckHealth();
    }

    private void CheckHealth()
    {
        if (_health == 0)
        {
            Died?.Invoke();
        }
    }
}
