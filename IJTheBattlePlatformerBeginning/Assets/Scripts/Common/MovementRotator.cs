using UnityEngine;

public class MovementRotator : MonoBehaviour
{
    private float _direction = 1;

    private float _turnDegree = 180f;

    public void SetDirection(Vector2 direction)
    {
        float tempDirection;

        tempDirection = direction.x;

        if (_direction != tempDirection && _direction != 0 && tempDirection != 0)
        {
            _direction = tempDirection;

            TurnObject();
        }
    }

    private void TurnObject() =>
        transform.Rotate(Vector2.up, _turnDegree);
}

