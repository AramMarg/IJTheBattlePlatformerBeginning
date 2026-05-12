using UnityEngine;

[RequireComponent(typeof(MovementRotator),typeof(GroundCheker))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private MovementRotator _movementRotator;
    [SerializeField] private GroundCheker _groundChecker;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _jumpSpeed;

    private Vector2 _inputAxis; 

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputReader.MoveClicked += OnMoveClicked;

        _inputReader.JumpClicked += OnJumpClicked;
    }

    private void OnDisable()
    {
        _inputReader.MoveClicked -= OnMoveClicked;

        _inputReader.JumpClicked -= OnJumpClicked;
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_inputAxis.x * _moveSpeed, _rigidbody.velocity.y);
    }

    private void OnMoveClicked(Vector2 inputAxis)
    {
        _movementRotator.SetDirection(inputAxis);

        _inputAxis = inputAxis;
    }

    private void OnJumpClicked(bool isJump)
    {
        if (_groundChecker.IsGround && isJump)
        {
            _rigidbody.AddForce(Vector2.up * _jumpSpeed , ForceMode2D.Impulse);
        }
    }
}
