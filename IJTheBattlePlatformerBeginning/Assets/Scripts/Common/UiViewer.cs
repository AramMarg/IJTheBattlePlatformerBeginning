using TMPro;
using UnityEngine;

public class UiViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _coinCountText;

    private int _coinAmount;

    private void OnEnable()
    {
        _player.CoinGot += OnCoinGot;
    }

    private void OnDisable()
    {
        _player.CoinGot -= OnCoinGot;
    }

    private void OnCoinGot()
    {
        _coinAmount++;

        _coinCountText.text = _coinAmount.ToString();
    }
}
