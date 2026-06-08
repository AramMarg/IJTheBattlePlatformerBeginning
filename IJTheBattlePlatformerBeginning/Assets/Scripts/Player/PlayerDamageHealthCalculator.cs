using UnityEngine;

public class PlayerDamageHealthCalculator : MonoBehaviour, IDamageable
{
    [SerializeField] private Armor _armor;
    [SerializeField] private Health _health;
    [SerializeField] private Inventory _inventory;

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

        if (currentHealth <= _health.Min)
        {
            int tempheal = _inventory.GetHeal();

            if (tempheal <= _health.Max)
            {
                currentHealth = tempheal;

                _inventory.SetHeal(_health.Min);
            }
            else
            {
                currentHealth = _health.Max;

                _inventory.SetHeal(tempheal - _health.Max);
            }
        }

        _health.SetCurrent(currentHealth);
    }
}
