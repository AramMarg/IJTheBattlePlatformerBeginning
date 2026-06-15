using UnityEngine;

public class EnemyDamageHealthCalculator : MonoBehaviour, IDamageable
{
    [SerializeField] private Armor _armor;
    [SerializeField] private Health _health;

    private int _minDamage = 1;

    public int TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return 0;
        }

        damage = _armor.ApplyArmor(damage);

        if (damage <= 0)
        {
            damage = _minDamage;
        }

        _health.TakeDamage(damage);

        return damage;
    }
}
