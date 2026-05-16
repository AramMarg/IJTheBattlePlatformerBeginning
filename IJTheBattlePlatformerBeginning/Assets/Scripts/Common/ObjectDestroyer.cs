using System.Collections;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    private Coroutine _coroutine;
    private WaitForSeconds _wait;
    private float _delay = 1f;

    private void Start()
    {
        _wait = new(_delay);
    }

    public void StartDestroy(GameObject gameObject)
    {
        if (gameObject == null)
            return;

        _coroutine = StartCoroutine(RunDestroy(gameObject));
    }

    private IEnumerator RunDestroy(GameObject gameObject)
    {
        yield return _wait;

        Destroy(gameObject);
    }
}
