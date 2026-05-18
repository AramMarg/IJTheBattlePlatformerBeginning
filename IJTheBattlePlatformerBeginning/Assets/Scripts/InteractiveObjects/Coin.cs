public class Coin : InteractObjectGeneric<Coin>, IInteractable<Coin>
{
    protected override Coin GetValue()
    {
        return this;
    }
}
