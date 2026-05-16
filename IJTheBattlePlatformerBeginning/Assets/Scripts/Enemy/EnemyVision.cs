using UnityEngine;
using System.Collections;
using System;

public class EnemyVision : MonoBehaviour
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
    }

    public void TurnOnDetect()
    {
        _needTarget = true;

        _coroutine = StartCoroutine(nameof(Detect));
    }

    public void TurnOffDetect() =>
        _needTarget = false;

    private IEnumerator Detect()
    {
        while (_needTarget)
        {
            Player player = null;

            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, _checkRadius);

            foreach (var item in items)
            {
                if (item.TryGetComponent(out player))
                {
                    PlayerFinded?.Invoke(player);

                    _needTarget = false;

                    StopCoroutine(_coroutine);

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
