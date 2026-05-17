using System;

public class Coin : InteractObject, IInteractable<Coin>
{
    public event Action<Coin> Interacted;
    
    public void Interact()
    {
        Interacted?.Invoke(this);
    }
}
