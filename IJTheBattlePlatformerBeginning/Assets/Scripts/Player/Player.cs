using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D), typeof (HealthHandler))]
[RequireComponent(typeof(Attacker))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private AttackTrigger _attackTrigger;

    private Attacker _attacker;

    private HealthHandler _healthHandler;

    public event Action CoinGot;

    private void Awake()
    {
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;

        _healthHandler = GetComponent<HealthHandler>();

        _attacker = GetComponent<Attacker>();
    }

    private void OnEnable()
    {
        _inputReader.AttackClicked += OnAttackClicked;
    }

    private void OnDisable()
    {
        _inputReader.AttackClicked -= OnAttackClicked;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            CoinGot?.Invoke();

            coin.Interact();
        }

        if (collision.TryGetComponent(out AidMeat aidMeat))
        {
            _healthHandler.Heal();

            aidMeat.Interact();
        }
    }

    private void OnAttackClicked(bool isAttack)
    {
        if (isAttack)
        {
          IDamageable target = _attackTrigger.Target; ;

            if (target != null)
            {
                _attacker.SetCanAttack(isAttack);

                _attacker.Attack(target);
            }
        }
        else
        {
            _attacker.SetCanAttack(!isAttack);
        }
    }
}
