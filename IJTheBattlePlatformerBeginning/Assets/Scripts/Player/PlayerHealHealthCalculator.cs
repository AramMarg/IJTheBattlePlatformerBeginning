using UnityEngine;

public class PlayerHealHealthCalculator : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Inventory _inventory;
    [SerializeField] private int _aidMeat = 10; 

    public void Heal()
    {
        if (_health.Current + _aidMeat <= _health.Max)
        {
            _health.SetCurrent(_health.Current + _aidMeat);
        }
        else 
        {
            _health.SetCurrent(_health.Max);

            _inventory.AddHeal(_health.Current + _aidMeat - _health.Max);
        }
    }
}
