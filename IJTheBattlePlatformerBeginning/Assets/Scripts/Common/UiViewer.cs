using TMPro;
using UnityEngine;

public class UiViewer : MonoBehaviour
{
    [SerializeField] private InteractObjectTrigger _interactObjectTrigger; 
    [SerializeField] private TextMeshProUGUI _coinCountText;

    private int _coinAmount;

    private void OnEnable()
    {
        _interactObjectTrigger.CoinGot += OnCoinGot;
    }

    private void OnDisable()
    {
        _interactObjectTrigger.CoinGot -= OnCoinGot;
    }

    private void OnCoinGot()
    {
        _coinAmount++;

        _coinCountText.text = _coinAmount.ToString();
    }
}
