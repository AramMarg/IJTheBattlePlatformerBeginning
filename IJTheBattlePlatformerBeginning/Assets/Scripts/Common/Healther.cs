using System;
using UnityEngine;

public class Healther : MonoBehaviour, IDamageable
{
    [SerializeField] private int _amor;

    private int _health = 100;
    private int _heal = 10;
    private int _healInBag;
    private int _min = 0;
    private int _max = 100;

    public event Action Died;

    public void TakeDamage(int damage)
    {
        damage -= _amor;

        if (damage <= 0)
        {
            damage = 1;
        }

        _health = Mathf.Clamp(_health - damage, _min, _max);

        if (_health == 0)
        {
            int tempHealFromBag = Mathf.Clamp(_health + _healInBag, _min, _max);

            _healInBag -= tempHealFromBag;

            _health = tempHealFromBag;
        }

        if (IsDie())
        {
            return;
        }
    }

    public void Heal()
    {
        if (_health == _max)
        {
            _healInBag += _heal;
        }
        else if (_health + _heal >= _max)
        {
            _health = _max;

            _healInBag += _health + _heal - _max;
        }
        else
        {
            _health += _heal;
        }
    }

    private bool IsDie()
    {
        if (_health == 0)
        {
            Died?.Invoke();

            return true;
        }

        return false;
    }
}
