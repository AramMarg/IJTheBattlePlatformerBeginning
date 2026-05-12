using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AidMeat : MonoBehaviour, IInteractable<AidMeat>
{
    public event Action<AidMeat> Interacted;

    private void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    public void RunDestroy()
    {
        Destroy(gameObject);
    }

    public void Interact()
    {
        Interacted?.Invoke(this);
    }
}

