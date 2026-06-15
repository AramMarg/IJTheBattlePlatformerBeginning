using System;
using UnityEngine;

public class InteractObjectTrigger : MonoBehaviour
{
    public event Action CoinGot;
    public event Action<AidMeat> AidMeatGot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out InteractObject interactObject))
        {
            if(interactObject is Coin coin)
            {
                CoinGot?.Invoke();

                coin.Interact();
            }
            else if (interactObject is AidMeat aidMeat)
            {
                AidMeatGot?.Invoke(aidMeat);

                aidMeat.Interact();
            }
        }
    }
}
