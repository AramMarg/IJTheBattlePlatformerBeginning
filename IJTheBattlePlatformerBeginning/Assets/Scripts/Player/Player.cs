using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof(Attacker))]
[RequireComponent(typeof(Healther), typeof(PlayerMover), typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerJumper), typeof(InteractObjectTrigger))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private WeaponAttackTrigger _weaponAttackTrigger;
    [SerializeField] private ObjectDestroyer _objectDestroyer;
    [SerializeField] private PlayerMover _playerMover;
    [SerializeField] private PlayerJumper _playerJumper;
    [SerializeField] private MovementRotator _movementRotator;
    [SerializeField] private GroundCheker _groundChecker;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private InteractObjectTrigger _interactObjectTrigger;
    [SerializeField] private UiViewer _uiViewer;

    private Attacker _attacker;
    private Healther _healther; 

    private void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        _attacker = GetComponent<Attacker>();
        _healther = GetComponent<Healther>();
    }

    private void OnEnable()
    {
        _inputReader.MoveClicked += OnMoveClicked;
        _inputReader.JumpClicked += OnJumpClicked;
        _inputReader.AttackClicked += OnAttackClicked;

        _healther.Died += OnDied;

        _interactObjectTrigger.CoinGot += OnACoinGot;
        _interactObjectTrigger.AidMeatGot += OnAidMeatGot;
    }

    private void OnACoinGot()
    {
        _uiViewer.CoinGot();
    }

    private void OnAidMeatGot()
    {
        _healther.Heal();
    }

    private void OnDisable()
    {
        _inputReader.MoveClicked -= OnMoveClicked;
        _inputReader.JumpClicked -= OnJumpClicked;
        _inputReader.AttackClicked -= OnAttackClicked;

        _healther.Died -= OnDied;

        _interactObjectTrigger.CoinGot -= OnACoinGot;
        _interactObjectTrigger.AidMeatGot -= OnAidMeatGot;

    }

    private void OnMoveClicked(Vector2 inputAxis)
    {
        _movementRotator.SetDirection(inputAxis);

        _playerMover.Move(inputAxis);

        _playerAnimator.Move(inputAxis);
    }

    private void OnJumpClicked(bool isJump)
    {
        if (_groundChecker.IsGround && isJump)
        {
            _playerJumper.Jump();

            _playerAnimator.Jump(isJump);
        }

        _playerAnimator.Jump(isJump);
    }

    private void OnAttackClicked(bool isAttack)
    {
        if (isAttack)
        {
          IDamageable target = _weaponAttackTrigger.Target; ;

            if (target != null)
            {
                _attacker.Attack(target);

                _playerAnimator.Attack(isAttack);
            }
        }

        _playerAnimator.Attack(isAttack);
    }

    private void OnDied()
    {
        _objectDestroyer.StartDestroy(gameObject);

        _playerAnimator.Died();
    }
}
