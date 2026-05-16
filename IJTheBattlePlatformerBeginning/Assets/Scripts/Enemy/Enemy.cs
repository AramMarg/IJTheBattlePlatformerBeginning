using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyVision), typeof(EnemyChaser))]
[RequireComponent(typeof(EnemyPatroller), typeof(Attacker), typeof(HealthHandler))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WeaponAttackTrigger _weaponAttackTrigger;
    [SerializeField] private ObjectDestroyer _objectDestroyer;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private EnemyVision _enemyVision; 
    private EnemyPatroller _enemyPatroller;
    private EnemyChaser _enemyChaser;
    private Attacker _attacker;
    private HealthHandler _healthHandler;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private float _delay = 0.3f;

    private Player _player;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;

        _enemyVision = GetComponent<EnemyVision>();
        _enemyPatroller = GetComponent<EnemyPatroller>();
        _enemyChaser = GetComponent<EnemyChaser>();
        _attacker = GetComponent<Attacker>();
        _healthHandler = GetComponent<HealthHandler>();
    }

    private void OnEnable()
    {
        _enemyVision.PlayerFinded += OnPlayerFinded;
        _enemyChaser.PlayerLosted += OnPlayerLosted;
        _healthHandler.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemyVision.PlayerFinded -= OnPlayerFinded;
        _enemyChaser.PlayerLosted -= OnPlayerLosted;
        _healthHandler.Died -= OnDied;
    }

    private void Start()
    {
        _wait = new(_delay);

        _enemyPatroller.RunPatrol();

        _enemyVision.TurnOnDetect();
    }

    private void Update()
    {
        if (_weaponAttackTrigger.Target != null && _coroutine == null)
        {
            _enemyChaser.StopChase();

            _enemyPatroller.StopPatrol();

            _enemyVision.TurnOffDetect();

            _coroutine = StartCoroutine(AttackWithTimer());
        }
        else
        {
            if (_player != null)
            {
                _enemyPatroller.StopPatrol();

                _enemyChaser.SetTarget(_player.transform);
                _enemyChaser.RunChase();
            }
            else
            {
                _enemyChaser.StopChase();

                _enemyPatroller.RunPatrol();

                _enemyVision.TurnOnDetect();
            }
        }
    }

    private void OnPlayerFinded(Player player)
    {
        _player = player;
    }

    private void OnPlayerLosted()
    {
        _player = null;
    }

    private IEnumerator AttackWithTimer()
    {
        bool isAttack = true;

        _enemyAnimator.Attack(isAttack);

        _attacker.Attack(_weaponAttackTrigger.Target);

        yield return _wait;

        _enemyAnimator.Attack(!isAttack);

        _coroutine = null;
    }

    private void OnDied()
    {
        _objectDestroyer.StartDestroy(gameObject);

        _enemyAnimator.Died();
    }
}
