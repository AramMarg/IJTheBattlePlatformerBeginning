using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int Current { get; private set; } = 100;
    public int Min { get; } = 0;
    public int Max { get; } = 100;

    public event Action<int> ValueChanged;
    public event Action Died;

    public void Heal(int healhealth)
    {
        SetCurrent(Mathf.Clamp(Current + healhealth,
                   Min, Max));
    }

    public void TakeDamage(int damage)
    {
        SetCurrent(Mathf.Clamp(Current - damage,
                    Min, Max));
    }

    private void SetCurrent(int current)
    {
        Current = current;

        if (Current <= Min)
        {
            Died?.Invoke();

            return;
        }

        ValueChanged?.Invoke(Current);
    }
}
