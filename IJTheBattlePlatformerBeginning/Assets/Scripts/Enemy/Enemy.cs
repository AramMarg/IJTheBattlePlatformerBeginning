using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyVision), typeof(EnemyChaser))]
[RequireComponent(typeof(EnemyPatroller), typeof(Attacker), typeof(Healther))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WeaponAttackTrigger _weaponAttackTrigger;
    [SerializeField] private ObjectDestroyer _objectDestroyer;
    [SerializeField] private EnemyAnimator _enemyAnimator;

    private EnemyVision _enemyVision; 
    private EnemyPatroller _enemyPatroller;
    private EnemyChaser _enemyChaser;
    private Attacker _attacker;
    private Healther _healther;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private float _delay = 0.3f;

    private Transform _target;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;

        _enemyVision = GetComponent<EnemyVision>();
        _enemyPatroller = GetComponent<EnemyPatroller>();
        _enemyChaser = GetComponent<EnemyChaser>();
        _attacker = GetComponent<Attacker>();
        _healther = GetComponent<Healther>();
    }

    private void OnEnable()
    {
        _enemyVision.TargetFinded += OnPlayerFinded;
        _enemyChaser.PlayerLosted += OnPlayerLosted;
        _healther.Died += OnDied;
    }

    private void OnDisable()
    {
        _enemyVision.TargetFinded -= OnPlayerFinded;
        _enemyChaser.PlayerLosted -= OnPlayerLosted;
        _healther.Died -= OnDied;
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
            if (_target != null)
            {
                _enemyPatroller.StopPatrol();

                _enemyChaser.SetTarget(_target.transform);
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

    private void OnPlayerFinded(Transform target)
    {
        _target = target;
    }

    private void OnPlayerLosted()
    {
        _target = null;
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
