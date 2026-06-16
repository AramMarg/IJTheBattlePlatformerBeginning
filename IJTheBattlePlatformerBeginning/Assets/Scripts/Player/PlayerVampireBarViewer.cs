using UnityEngine;
using UnityEngine.UI;

public class PlayerVampireBarViewer : MonoBehaviour
{
    [SerializeField] private PlayerVampirer _playerVampirer;   
    [SerializeField] private Canvas _canvas;
    [SerializeField] private Image _fillImigeBar;

    private float _fillMax = 1f;

    private void OnEnable()
    {
        _playerVampirer.VampirismRan += OnVampirismRan;
        _playerVampirer.VampirismReloaded += OnVampirismReloaded;
    }

    private void OnDisable()
    {
        _playerVampirer.VampirismRan -= OnVampirismRan;
        _playerVampirer.VampirismReloaded -= OnVampirismReloaded;
    }

    private void Awake()
    {
        _canvas.enabled = false;
    }

    private void OnVampirismRan(float elapsedTime)
    {
        _canvas.enabled = true;

        _fillImigeBar.fillAmount = _fillMax - (elapsedTime / _playerVampirer.AmountTimeVampirism);
    }

    private void OnVampirismReloaded(float elapsedTime)
    {
        _fillImigeBar.fillAmount = elapsedTime / _playerVampirer.AmountTimeReloadVampirism;

        if (Mathf.Approximately(_fillImigeBar.fillAmount, _fillMax))
        {
            _canvas.enabled = false;
        }
    }
}
