using System;
using System.Collections;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private float _delay = 0.2f;

    private bool _needTarget;
    private float _checkRadius = 2f;

    public event Action<Player> PlayerFinded;

    private void Start()
    {
        _wait = new(_delay);

        _coroutine = StartCoroutine(nameof(Detect));
    }

    public void TurnOnDetector() =>
        _needTarget = true;

    public void TurnOffDetector() =>
        _needTarget = false;

    private IEnumerator Detect()
    {
        while (_needTarget)
        {
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, _checkRadius);

            foreach (var item in items)
            {
                if (item.TryGetComponent(out Player player))
                {
                    PlayerFinded?.Invoke(player);

                    _needTarget = false;

                    break;
                }
            }

            yield return _wait;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    } 
}
