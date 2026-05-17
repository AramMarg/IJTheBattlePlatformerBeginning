using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private float _jumpSpeed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpSpeed, ForceMode2D.Impulse);
    }
}
