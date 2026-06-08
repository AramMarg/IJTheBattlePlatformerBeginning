using UnityEngine;

public class MovementRotator : MonoBehaviour
{
    private float _direction = 1;

    private float _turnDegree = 180f;

    public void SetDirection(Vector2 direction)
    {
        float tempDirection;

        tempDirection = direction.x;

        if (_direction != tempDirection &&
            (Mathf.Approximately(_direction, 0) == false )
            && (Mathf.Approximately(tempDirection, 0) == false))
        {
            _direction = tempDirection;

            TurnObject();
        }
    }

    private void TurnObject() =>
        transform.Rotate(Vector2.up, _turnDegree);
}

