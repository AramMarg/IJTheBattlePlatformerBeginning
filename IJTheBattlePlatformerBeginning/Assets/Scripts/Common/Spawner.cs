using UnityEngine;

public class Spawner<T> : MonoBehaviour where T : Component
{
    public T Create(T prefab, Vector2 position)
    {
        return Instantiate(prefab, position, Quaternion.identity);
    }
}
