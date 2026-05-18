using System;
using UnityEngine;

public class InteractObjectTrigger : MonoBehaviour
{
    public event Action CoinGot;
    public event Action AidMeatGot;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision);
        if(collision.TryGetComponent(out InteractObject interactObject))
        {
            if(interactObject is Coin coin)
            {
                print(interactObject);

                print(interactObject.gameObject);

                CoinGot?.Invoke();

                coin.Interact();
            }
            else if (interactObject is AidMeat aidMeat)
            {
                AidMeatGot?.Invoke();

                aidMeat.Interact();
            }
        }
    }
}
