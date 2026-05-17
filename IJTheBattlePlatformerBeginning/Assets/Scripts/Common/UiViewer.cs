using TMPro;
using UnityEngine;

public class UiViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _coinCountText;

    private int _coinAmount;

    public void CoinGot()
    {
        _coinAmount++;

        _coinCountText.text = _coinAmount.ToString();
    }
}
