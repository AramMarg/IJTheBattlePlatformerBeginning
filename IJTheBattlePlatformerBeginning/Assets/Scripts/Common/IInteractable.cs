using System;

public interface IInteractable<T> where T: IInteractable<T>
{
    public event Action<T> Interacted;

    public void RunDestroy();

    public void Interact();
}
