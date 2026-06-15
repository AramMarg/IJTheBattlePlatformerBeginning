using UnityEngine;

public class PlayerDamageHealthCalculator : MonoBehaviour, IDamageable
{
    [SerializeField] private Armor _armor;
    [SerializeField] private Health _health;
    [SerializeField] private Inventory _inventory;

    private int _minDamage = 1;

    public int TakeDamage(int damage)
    {
        int currentHealth;

        if (_health.Current <= 0)
        {
            return 0;
        }

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

        currentHealth = _health.Current; 

        if (currentHealth <= _health.Min)
        {
            int tempheal = _inventory.GetHeal();

            if (tempheal <= _health.Max)
            {
                _health.Heal(tempheal);

                _inventory.SetHeal(_health.Min);
            }
            else
            {
                _health.Heal(_health.Max); ;

                _inventory.SetHeal(tempheal - _health.Max);
            }
        }

        return damage;
    }
}
