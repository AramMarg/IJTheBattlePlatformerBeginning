using System;

public class AidMeat : InteractObject, IInteractable<AidMeat>
{
    public event Action<AidMeat> Interacted;

    public void Interact()
    {
        Interacted?.Invoke(this);
    }
}

