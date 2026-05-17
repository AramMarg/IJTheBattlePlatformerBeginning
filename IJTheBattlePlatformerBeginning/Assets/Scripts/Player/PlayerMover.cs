using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

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
}
