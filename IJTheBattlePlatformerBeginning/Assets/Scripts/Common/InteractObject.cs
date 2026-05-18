using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class InteractObject : MonoBehaviour
{
    protected void Awake()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }
}

