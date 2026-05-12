using UnityEngine;

public class EnemyAnimatorData 
{
    public static class Parametrs
    {
        public static readonly int IsAttack = Animator.StringToHash(nameof(IsAttack));
        public static readonly int Die = Animator.StringToHash(nameof(Die));
    }
}
