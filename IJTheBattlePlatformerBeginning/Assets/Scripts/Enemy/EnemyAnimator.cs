using System;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private HealthHandler _healthHandler;
    [SerializeField] private Enemy _enemy; 

    private Animator _animatior;

    private void Awake()
    {
        _animatior = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _healthHandler.Died += OnDied;
        _enemy.AttackStarted += OnAttackStarted;
    }

    private void OnDisable()
    {
        _healthHandler.Died -= OnDied;
        _enemy.AttackStarted -= OnAttackStarted;
    }

    private void OnAttackStarted(bool isAttack)
    {
        _animatior.SetBool(EnemyAnimatorData.Parametrs.IsAttack, isAttack);
    }

    private void OnDied()
    {
        _animatior.SetTrigger(EnemyAnimatorData.Parametrs.Die);
    }
}
