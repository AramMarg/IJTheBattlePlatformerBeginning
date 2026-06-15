using UnityEngine;

public class PlayerHealHealthCalculator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Inventory _inventory;

    public void Heal(int aidMeatAmount)
    {
        if (_health.Current + aidMeatAmount <= _health.Max)
        {
            _health.Heal(_health.Current + aidMeatAmount);
        }
        else 
        {
            _health.Heal(_health.Max);

            _inventory.AddHeal(_health.Current + aidMeatAmount - _health.Max);
        }
    }
}
