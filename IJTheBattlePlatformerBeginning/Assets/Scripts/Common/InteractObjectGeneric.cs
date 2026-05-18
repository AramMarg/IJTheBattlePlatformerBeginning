using System;

public abstract class InteractObjectGeneric<T> : InteractObject where T : IInteractable<T>
{
    public event Action<T> Interacted;

    public void Interact()
    {
        Interacted?.Invoke(GetValue());
    }

    protected abstract T GetValue();
}
