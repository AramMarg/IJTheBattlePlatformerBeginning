public class AidMeat : InteractObjectGeneric<AidMeat>, IInteractable<AidMeat>
{
    public int AidMeatCount { get; private set; } = 10;

    protected override AidMeat GetValue()
    {
        return this;
    }
}
