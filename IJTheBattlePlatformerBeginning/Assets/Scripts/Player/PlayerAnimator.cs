using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private HealthHandler _healthHandler;
    [SerializeField] private GroundCheker _groundChecker;

    private Animator _animatior;

    private int _forAnimation;

    private void Awake()
    {
        _animatior = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _inputReader.MoveClicked += OnMoveClicked;

        _inputReader.JumpClicked += OnJumpClicked;

        _inputReader.AttackClicked += OnAttackClicked;

        _healthHandler.Died += OnDied;
    }

    private void OnDisable()
    {
        _inputReader.MoveClicked -= OnMoveClicked;

        _inputReader.JumpClicked -= OnJumpClicked;

        _inputReader.AttackClicked -= OnAttackClicked;

        _healthHandler.Died -= OnDied;
    }

    private void OnAttackClicked(bool isAttack)
    {
        _animatior.SetBool(PlayerAnimatorData.Parametrs.IsAttack, isAttack);
    }

    private void OnJumpClicked(bool isJump)
    {
        if (_groundChecker.IsGround && isJump)
        {
            _animatior.SetBool(PlayerAnimatorData.Parametrs.IsJump, isJump);
        }

        _animatior.SetBool(PlayerAnimatorData.Parametrs.IsJump, isJump);
    }

    private void OnMoveClicked(Vector2 vector)
    {
        if (vector == Vector2.zero)
        {
            _forAnimation = 0;

            _animatior.SetInteger(PlayerAnimatorData.Parametrs.WalkOrIdle, _forAnimation);
        }
        else
        {
            _forAnimation = 1;

            _animatior.SetInteger(PlayerAnimatorData.Parametrs.WalkOrIdle, _forAnimation);
        }
    }

    private void OnDied()
    {
        _animatior.SetTrigger(PlayerAnimatorData.Parametrs.Die);
    }
}
