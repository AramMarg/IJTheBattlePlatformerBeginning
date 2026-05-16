using UnityEngine;

public class Attacker : MonoBehaviour
{
    [SerializeField] private int _damage;

    public void Attack(IDamageable target)
    {
         target.TakeDamage(_damage);
    }
}
