using System;
using UnityEngine;

public class InteractObjectTrigger : MonoBehaviour
{
    [SerializeField] private HealthHandler _healthHandler;

    public event Action CoinGot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            CoinGot?.Invoke();

                coin.Interact();
        }

        if (collision.TryGetComponent(out AidMeat aidMeat))
        {
            _healthHandler.Heal();

            aidMeat.Interact();
        }
    }
}
