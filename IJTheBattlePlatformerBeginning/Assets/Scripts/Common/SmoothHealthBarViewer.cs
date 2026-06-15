using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBarViewer : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Image _fillImige;
    [SerializeField] private float _smoothSpeed = 5f;

    private float _fillConvertCount = 100f;

    private Coroutine _coroutine;

    protected void OnEnable()
    {
        _health.ValueChanged += OnHealthChanged;
    }

    protected void OnDisable()
    {
        _health.ValueChanged -= OnHealthChanged;
    }

    private void Start()
    {
        _fillImige.fillAmount = _health.Max / _fillConvertCount;
    }

    public void OnHealthChanged(int helth)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(SmoothFill(helth));
    }

    private IEnumerator SmoothFill(int helth)
    {
        float tempFill = _fillImige.fillAmount;
        float elapsedTime = 0;

        while (Mathf.Approximately(_fillImige.fillAmount, helth / _fillConvertCount) == false)
        {
            elapsedTime += Time.deltaTime;

            _fillImige.fillAmount = Mathf.Lerp
                (tempFill, helth / _fillConvertCount,
               elapsedTime * _smoothSpeed);

            yield return null;
        }
    }

    //with MoveTowards exaple
    //private IEnumerator SmoothFill(int helth)
    //{ 
    //while (Mathf.Approximately(_fillImige.fillAmount,
    //    helth / _fillConvertCount) == false)
    //    {
    //        _fillImige.fillAmount = Mathf.Lerp
    //            (_fillImige.fillAmount, helth / _fillConvertCount,
    //          _smoothSpeed);
    //        yield return null;
    //    }
    //}
}
