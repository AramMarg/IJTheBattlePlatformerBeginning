using System;
using System.Collections;
using UnityEngine;

[RequireComponent (typeof (Collider2D), typeof(Rigidbody2D))]
[RequireComponent(typeof(EnemyDetector), typeof(EnemyChaser))]
[RequireComponent(typeof(EnemyPatroller), typeof(Attacker))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private AttackTrigger _attackTrigger;

    private EnemyDetector _enemyDetector;
    private EnemyPatroller _enemyPatroller;
    private EnemyChaser _enemyChaser;
    private Attacker _attacker;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private float _delay = 0.3f;

    private Player _player;

    public event Action<bool> AttackStarted;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().freezeRotation = true;

        _enemyDetector = GetComponent<EnemyDetector>();
        _enemyPatroller = GetComponent<EnemyPatroller>();
        _enemyChaser = GetComponent<EnemyChaser>();
        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _enemyDetector.PlayerFinded += OnPlayerFinded;

        _enemyChaser.PlayerLosted += OnPlayerLosted;
    }

    private void OnDisable()
    {
        _enemyDetector.PlayerFinded -= OnPlayerFinded;
        _enemyChaser.PlayerLosted -= OnPlayerLosted;
    }

    private void Start()
    {
        _wait = new(_delay);
    }

    private void Update()
    {
        if (_attackTrigger.Target != null && _coroutine == null)
        {
            _enemyChaser.StopChase();

            _enemyPatroller.StopPatrol();

            _enemyDetector.TurnOffDetector();

            _coroutine = StartCoroutine(AttackWithTimer());
        }
        else
        {
            if (_player != null)
            {
                _enemyChaser.SetTarget(_player.transform);
                _enemyChaser.RunChase();
                _enemyDetector.TurnOnDetector();

            }
            else
            {
                _enemyChaser.StopChase();
                _enemyPatroller.RunPatrol();
                _enemyDetector.TurnOnDetector();
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
        bool canAttack = true;

        AttackStarted?.Invoke(canAttack);

        _attacker.SetCanAttack(canAttack);

        _attacker.Attack(_attackTrigger.Target);

        yield return _wait;

        canAttack = false;

        AttackStarted?.Invoke(canAttack);

        _attacker.SetCanAttack(canAttack);

        _coroutine = null;
    }
}
