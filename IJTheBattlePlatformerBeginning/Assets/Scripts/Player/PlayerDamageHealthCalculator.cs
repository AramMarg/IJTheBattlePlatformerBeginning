using UnityEngine;

public class PlayerDamageHealthCalculator : MonoBehaviour, IDamageable
{
    [SerializeField] private Armor _armor;
    [SerializeField] private Health _health;
    [SerializeField] private Inventory _inventory;

    private int _minDamage = 1;

    public int TakeDamage(int damage)
    {
        int previousHealth;

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

        previousHealth = _health.Current;

        if (previousHealth <= _health.Min)
        {
            int tempheal = _inventory.GetHeal();

            _health.Heal(tempheal);

            //check
            _inventory.SetHeal(tempheal - (_health.Current - previousHealth));
        }

            return damage;
    }
}
