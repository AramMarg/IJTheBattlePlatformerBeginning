using UnityEngine;

public class Armor : MonoBehaviour
{
    [SerializeField] private int _armor = 5;

    public int ApplyArmor(int damage)
    {
        return damage -= _armor;
    }
}

