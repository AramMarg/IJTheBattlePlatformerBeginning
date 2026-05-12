using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage;

    public bool CanAttack { get; private set; }

    public void SetCanAttack(bool canAttack)
    {
        CanAttack = canAttack;
    }

    public void Attack(IDamageable target)
    {
        if (CanAttack)
        {
            target.TakeDamage(_damage);
        }
    }
}
