using TMPro;
using UnityEngine;

public class UiViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private TextMeshProUGUI _aidMeatCountText;

    private int _coinAmount;

    public void CoinGot()
    {
        _coinAmount++;

        _coinCountText.text = _coinAmount.ToString();
    }

    public void AidMeatGot(int aidMeat)
    {
        _aidMeatCountText.text = aidMeat.ToString();
    }
}
