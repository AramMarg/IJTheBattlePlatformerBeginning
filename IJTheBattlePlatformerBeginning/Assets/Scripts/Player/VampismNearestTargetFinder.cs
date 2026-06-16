using UnityEngine;

public class VampismNearestTargetFinder : MonoBehaviour
{
    [SerializeField] private LayerMask _enemyLayerMask;

    private float _checkRadius = 2f;
    private int _maxEnemy = 5;
    private Collider2D[] _items;

    private void Awake()
    {
        _items = new Collider2D[_maxEnemy];
    }

    public bool TryFindNearestTarget(out IDamageable target)
    {
        target = null;

        float nearestDistance = float.MaxValue;

        int tempCountItems = Physics2D.OverlapCircleNonAlloc
            (transform.position, _checkRadius, _items, _enemyLayerMask);

        if (tempCountItems == 0)
        {
            return false;
        }

        for (int i = 0; i < tempCountItems; i++)
        {
            Collider2D tempItem = _items[i];

            if (tempItem.TryGetComponent(out IDamageable tempTarget))
            {
                float tempDistance = (transform.position -
                    tempItem.gameObject.transform.position).sqrMagnitude;

                if (tempDistance < nearestDistance)
                {
                    target = tempTarget;
                }
            }
        }

        return target != null;
    }
}
