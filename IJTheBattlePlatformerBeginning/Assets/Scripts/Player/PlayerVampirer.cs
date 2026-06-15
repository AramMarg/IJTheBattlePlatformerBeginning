using System;
using UnityEngine;
using System.Collections;

public class PlayerVampirer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private LayerMask _enemyLayerMask;

    private Coroutine _coroutine;

    private int _takeAwayHealthCount = 15;

    private float _checkRadius = 2f;

    private int _maxEnemy = 5;

    private bool isRunVampirism;

    private Collider2D[] items;

    public int AmountTimeVampirism { get; } = 6;
    public int AmountTimeReloadVampirism { get; } = 4;

    public event Action VampirismRan;
    public event Action VampirismReloaded;

    private void Awake()
    {
        items = new Collider2D[_maxEnemy];
    }

    public void StartVampirism()
    {
        isRunVampirism = !isRunVampirism;

        if (isRunVampirism)
        {
            _coroutine = StartCoroutine(nameof(RunVampirism));
        }
    }

    private IEnumerator RunVampirism()
    {
        float elapsedTime = 0;

        while (elapsedTime <= AmountTimeVampirism)
        {
            elapsedTime += Time.deltaTime;

            if (TryFindNearestTarget(out IDamageable target, out int index))
            {
                TakeAwayHealth(target, index);
            }

            VampirismRan?.Invoke();

            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime <= AmountTimeReloadVampirism)
        {
            elapsedTime += Time.deltaTime;

            VampirismReloaded?.Invoke();

            yield return null;
        }

        isRunVampirism = !isRunVampirism;
    }

    private void TakeAwayHealth(IDamageable target, int index)
    {
        if (items[index].TryGetComponent(out Health targetHealth))
        {
            if (targetHealth.Current <= 0)
            {
                return;
            }
            else
            {
                int tempHealth;

                tempHealth = target.TakeDamage(_takeAwayHealthCount);

                _health.Heal(tempHealth);
            }
        }
    }

    private bool TryFindNearestTarget(out IDamageable target, out int index)
    {
        target = null;

        index = 0;

        float nearestDistance = float.MaxValue;

        int tempCountItems = Physics2D.OverlapCircleNonAlloc
            (transform.position, _checkRadius, items, _enemyLayerMask);

        if (tempCountItems == 0)
        {
            return false;
        }

        for (int i = 0; i < tempCountItems; i++)
        {
            float tempDistance;

            Collider2D tempItem = items[i];

            if (tempItem.TryGetComponent(out IDamageable tempTarget))
            {
                tempDistance = (transform.position -
                    tempItem.gameObject.transform.position).sqrMagnitude;

                if (tempDistance < nearestDistance)
                {
                    target = tempTarget;

                    index = i;
                }
            }
        }

        return target != null;
    }
}

