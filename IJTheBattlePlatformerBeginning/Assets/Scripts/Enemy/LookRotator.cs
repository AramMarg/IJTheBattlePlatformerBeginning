using UnityEngine;

public class LookRotator : MonoBehaviour
{
    public void SetLook(Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}


