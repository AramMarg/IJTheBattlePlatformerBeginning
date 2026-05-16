using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class WeaponAttackTrigger : MonoBehaviour
{
    public IDamageable Target { get; private set; }

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable target))
        {
            Target = target;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamageable _))
        {
            Target = null;
        }
    }
}
