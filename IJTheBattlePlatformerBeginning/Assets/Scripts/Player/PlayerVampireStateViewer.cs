using UnityEngine;
using UnityEngine.UI;

public class PlayerVampireStateViewer : MonoBehaviour
{
    [SerializeField] private PlayerVampirer _playerVampirer;   
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SpriteRenderer _circle;
    [SerializeField] private Image _fillImigeBar;

    private float _fillMin = 0f;
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

    private void Start()
    {
        _canvas.enabled = false;
        _circle.gameObject.SetActive(false);
    }

    private void OnVampirismRan(float decreaseFillCount)
    {
        _canvas.enabled = true;

        _circle.gameObject.SetActive(true);

        _fillImigeBar.fillAmount -= Mathf.Clamp(decreaseFillCount, _fillMin, _fillMax);
    }

    private void OnVampirismReloaded(float increaseFillCount)
    {
        _circle.gameObject.SetActive(false);

        _fillImigeBar.fillAmount += Mathf.Clamp(increaseFillCount, _fillMin, _fillMax);

        if (Mathf.Approximately(_fillImigeBar.fillAmount, _fillMax))
        {
            _canvas.enabled = false;
        }
    }
}
