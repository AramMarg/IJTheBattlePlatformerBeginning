using UnityEngine;

public class PlayerVampireZoneViewer : MonoBehaviour
{
    [SerializeField] private PlayerVampirer _playerVampirer;
    [SerializeField] private SpriteRenderer _circle;

    private float _checkRadius = 2f;

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
        _circle.gameObject.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _checkRadius);
    }

    private void OnVampirismRan(float decreaseFillCount)
    {
        _circle.gameObject.SetActive(true);        
    }

    private void OnVampirismReloaded(float increaseFillCount)
    {
        _circle.gameObject.SetActive(false);
    }
}
