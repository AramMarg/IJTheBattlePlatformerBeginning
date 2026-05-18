using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animatior;

    private int _forAnimation;

    private void Awake()
    {
        _animatior = GetComponent<Animator>();
    }

    public void Move(Vector2 vector)
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

    public void Jump(bool isJump)
    {
        if (isJump)
        {
            _animatior.SetBool(PlayerAnimatorData.Parametrs.IsJump, isJump);
        }

        _animatior.SetBool(PlayerAnimatorData.Parametrs.IsJump, isJump);
    }

    public void Attack(bool isAttack)
    {
        _animatior.SetBool(PlayerAnimatorData.Parametrs.IsAttack, isAttack);
    }

    public void Died()
    {
        _animatior.SetTrigger(PlayerAnimatorData.Parametrs.Die);
    }
}
