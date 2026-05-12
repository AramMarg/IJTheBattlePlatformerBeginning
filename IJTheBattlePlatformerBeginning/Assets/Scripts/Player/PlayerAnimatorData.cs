using UnityEngine;

public static class PlayerAnimatorData 
{
    public static class Parametrs
    {
        public static readonly int WalkOrIdle = Animator.StringToHash(nameof(WalkOrIdle));
        public static readonly int IsJump = Animator.StringToHash(nameof(IsJump));
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
        public static readonly int Die = Animator.StringToHash(nameof(Die));
    }
}
