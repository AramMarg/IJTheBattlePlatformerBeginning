using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private Animator _animatior;

    private void Awake()
    {
        _animatior = GetComponent<Animator>();
    }

    public void Attack(bool isAttack)
    {
        _animatior.SetBool(EnemyAnimatorData.Parametrs.IsAttack, isAttack);
    }

    public void Died()
    {
        _animatior.SetTrigger(EnemyAnimatorData.Parametrs.Die);
    }
}
