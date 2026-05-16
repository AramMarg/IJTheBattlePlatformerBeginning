using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Coin : MonoBehaviour, IInteractable<Coin>
{
    public event Action<Coin> Interacted;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void Interact()
    {
        Interacted?.Invoke(this);
    }
}
