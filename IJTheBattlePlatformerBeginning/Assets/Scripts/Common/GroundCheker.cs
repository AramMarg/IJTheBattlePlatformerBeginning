using UnityEngine;

public class GroundCheker : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private LayerMask _ground;

    private float _checkRadius = 0.2f;

    public bool IsGround { get; private set; }

    private void FixedUpdate()
    {
        IsGround = Physics2D.OverlapCircle(_groundCheckPoint.position, _checkRadius, _ground);
    }

    private void OnDrawGizmos()
    {
        if (_groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_groundCheckPoint.position, _checkRadius);
        }
    }
}
