using UnityEngine;

public class EnemyDamageHealthCalculator : MonoBehaviour, IDamageable
{
    [SerializeField] private Armor _armor;
    [SerializeField] private Health _health;

    private int _minDamage = 1;

    public void TakeDamage(int damage)
    {
        int currentHealth = _health.Current;

        if (damage < 0)
        {
            return;
        }

        damage = _armor.ApplyArmor(damage);

        if (damage <= 0)
        {
            damage = _minDamage;
        }

        currentHealth -= damage;

        _health.SetCurrent(currentHealth);
    }
}
