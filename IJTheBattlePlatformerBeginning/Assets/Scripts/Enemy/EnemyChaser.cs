using System;
using UnityEngine;

[RequireComponent(typeof(GroundCheker))]
public class EnemyChaser : MonoBehaviour
{
    [SerializeField] private LookRotator _lookRotator;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _chaseRange = 5f;

    private Transform _target;
    private bool _runChase;

    public event Action PlayerLosted;

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void RunChase()
    {
        _runChase = true;

        Chase();
    }

    public void StopChase() =>
        _runChase = false;

    private void Chase()
    {
        if (_runChase)
        {
            if (_target == null)
                return;

            if (IsTargetInRange())
            {
                _lookRotator.SetLook(_target);

                Vector3 direction = (_target.position - transform.position).normalized;
                transform.position += direction * _speed * Time.deltaTime;
            }
            else
            {
                _target = null;

                PlayerLosted?.Invoke();
            }
        }
    }

    private bool IsTargetInRange()
    {
        if (_target == null)
            return false;

        return (_target.position - transform.position).sqrMagnitude <= Mathf.Pow(_chaseRange,2);
    }
}
