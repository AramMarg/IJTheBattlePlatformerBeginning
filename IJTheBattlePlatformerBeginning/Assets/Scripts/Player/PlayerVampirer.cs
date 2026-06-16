using System;
using UnityEngine;
using System.Collections;

public class PlayerVampirer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private VampismNearestTargetFinder _vampismNearestTargetFinder; 

    private Coroutine _coroutine;
    private int _takeAwayHealthCount = 5;   
    private bool _isRunVampirism;
    //private float _fillConvertCount = 100f;
    //private float _increaseFillCount;
    //private float _decreaseFillCount;
    //private float elapsedTime = 0;

    public int AmountTimeVampirism { get; } = 6;
    public int AmountTimeReloadVampirism { get; } = 4;

    public event Action<float> VampirismRan;
    public event Action<float> VampirismReloaded;

    public void StartVampirism()
    {
        _isRunVampirism = !_isRunVampirism;

        if (_isRunVampirism)
        {
            _coroutine = StartCoroutine(RunVampirism());
        }
    }

    private IEnumerator RunVampirism()
    {
        yield return UseVampirism();

        yield return ReloadVampirism();

        _isRunVampirism = !_isRunVampirism;
    }

    private IEnumerator UseVampirism()
    {
        float elapsedTime = 0;

        while (elapsedTime <= AmountTimeVampirism)
        {
            elapsedTime += Time.deltaTime;

            if (_vampismNearestTargetFinder.TryFindNearestTarget(out IDamageable target))
            {
                TakeAwayHealth(target);
            }

            VampirismRan?.Invoke(elapsedTime);

            yield return null;
        }

        VampirismRan?.Invoke(AmountTimeVampirism);
    }

    private IEnumerator ReloadVampirism()
    {
        float elapsedTime = 0;

        while (elapsedTime <= AmountTimeReloadVampirism)
        {
            elapsedTime += Time.deltaTime;

            VampirismReloaded?.Invoke(elapsedTime);

            yield return null;
        }

        VampirismReloaded?.Invoke(AmountTimeReloadVampirism);

    }

    private void TakeAwayHealth(IDamageable target)
    {
            int tempHealth;

            tempHealth = target.TakeDamage(_takeAwayHealthCount);

            _health.Heal(tempHealth);
    }
}

