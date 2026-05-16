using UnityEngine;

[RequireComponent(typeof(MovementRotator),typeof(GroundCheker))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;

    private Vector2 _inputAxis; 

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_inputAxis.x * _moveSpeed, _rigidbody.velocity.y);
    }

    public  void Move(Vector2 inputAxis)
    {
        _inputAxis = inputAxis;
    }

    public void Jump(bool isJump)
    {
        _rigidbody.AddForce(Vector2.up * _jumpSpeed , ForceMode2D.Impulse);
    }
}
