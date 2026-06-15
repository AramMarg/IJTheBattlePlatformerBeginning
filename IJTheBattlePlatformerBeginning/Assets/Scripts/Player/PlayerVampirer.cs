using System;
using UnityEngine;
using System.Collections;

public class PlayerVampirer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private LayerMask _enemyLayerMask;

    private Coroutine _coroutine;

    private int _takeAwayHealthCount = 5;

    private float _checkRadius = 2f;

    private int _maxEnemy = 5;

    private bool isRunVampirism;

    private Collider2D[] items;

    private int _amountTimeVampirism = 6;
    private int _amountTimeReloadVampirism = 4;
    private float _fillConvertCount = 100f;
    private float _increaseFillCount;
    private float _decreaseFillCount;

    public event Action<float> VampirismRan;
    public event Action<float> VampirismReloaded;

    private void Awake()
    {
        items = new Collider2D[_maxEnemy];

        _decreaseFillCount = _amountTimeVampirism / _fillConvertCount;
        _increaseFillCount = _amountTimeReloadVampirism / _fillConvertCount;
    }

    public void StartVampirism()
    {
        isRunVampirism = !isRunVampirism;

        if (isRunVampirism)
        {
            _coroutine = StartCoroutine(RunVampirism());
        }
    }

    private IEnumerator RunVampirism()
    {
        float elapsedTime = 0;

        while (elapsedTime <= _amountTimeVampirism)
        {
            elapsedTime += Time.deltaTime;

            if (TryFindNearestTarget(out IDamageable target))
            {
                TakeAwayHealth(target);
            }

            VampirismRan?.Invoke(_decreaseFillCount);

            yield return null;
        }

        elapsedTime = 0;

        while (elapsedTime <= _amountTimeReloadVampirism)
        {
            elapsedTime += Time.deltaTime;

            VampirismReloaded?.Invoke(_increaseFillCount);

            yield return null;
        }

        isRunVampirism = !isRunVampirism;
    }

    private void TakeAwayHealth(IDamageable target)
    {
            int tempHealth;

            tempHealth = target.TakeDamage(_takeAwayHealthCount);

            _health.Heal(tempHealth);
      
    }

    private bool TryFindNearestTarget(out IDamageable target)
    {
        target = null;

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
                }
            }
        }

        return target != null;
    }
}

