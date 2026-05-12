using UnityEngine;

[RequireComponent(typeof(GroundCheker), typeof(LookRotator))]
public class EnemyPatroller : MonoBehaviour
{
    [SerializeField] private GroundCheker _groundCheker;
    [SerializeField] private LookRotator _lookRotator; 
    [SerializeField] private Transform[] _patrolPoints;
    [SerializeField] private float _speed = 2f;

    private int _currentIndex = 0;

    private bool _isRunPatrol;

    public void RunPatrol()
    {
        _isRunPatrol = true;

        Patrol();
    }

    public void StopPatrol()
    {
        _isRunPatrol = false;
    }

    private void Patrol()
    {
        if (_patrolPoints.Length == 0)
            return;

        if (_isRunPatrol)
        {
            _lookRotator.SetLook(_patrolPoints[_currentIndex]);

            if (_groundCheker.IsGround)
            {
                if (transform.position == _patrolPoints[_currentIndex].position)
                {
                    _currentIndex = ++_currentIndex % _patrolPoints.Length;

                    _lookRotator.SetLook(_patrolPoints[_currentIndex]);
                }

                transform.position = Vector2.MoveTowards(transform.position, _patrolPoints[_currentIndex].position, _speed * Time.deltaTime);
            }
        }
    }
}
