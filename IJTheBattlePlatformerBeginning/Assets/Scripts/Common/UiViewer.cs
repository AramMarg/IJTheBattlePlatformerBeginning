using TMPro;
using UnityEngine;

public class UiViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TextMeshProUGUI _coinCountText;
    [SerializeField] private TextMeshProUGUI _aidMeatCountText;

    private int _coinAmount;

    private void OnEnable()
    {
        _player.CoinGot += OnCoinGot;
        _player.AidMeatGot += OnAidMeatGot;
    }

    private void OnDisable()
    {
        _player.CoinGot -= OnCoinGot;
        _player.AidMeatGot -= OnAidMeatGot;
    }

    public void OnCoinGot()
    {
        _coinAmount++;

        _coinCountText.text = _coinAmount.ToString();
    }

    public void OnAidMeatGot(int aidMeat)
    {
        _aidMeatCountText.text = aidMeat.ToString();
    }
}
