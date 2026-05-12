using System.Collections;
using UnityEngine;

public class InteractObjectHandler<T> : MonoBehaviour where T : Component, IInteractable<T>
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private T _prefab;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private float _delay = 5f;

    private void Awake()
    {
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            Create(_spawnPoints[i].position);
        }
    }

    private void Start()
    {
        _wait = new(_delay);
    }

    public void OnInteract(T item)
    {
        item.Interacted -= OnInteract;

        _coroutine = StartCoroutine(CreateNew(item.transform.position));

        item.RunDestroy();
    }

    private IEnumerator CreateNew(Vector2 position)
    {
        yield return _wait;

        Create(position);
    }

    private void Create(Vector2 position)
    {
        T item = _spawner.Create(_prefab, position);

        item.Interacted += OnInteract;
    }
}
