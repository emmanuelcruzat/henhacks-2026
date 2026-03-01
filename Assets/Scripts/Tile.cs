using System.Collections;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied;

    public Color greenColor;
    public Color redColor;

    private SpriteRenderer sr;
    private Vector3 normalScale;
    private Coroutine scaleRoutine;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        normalScale = transform.localScale;
    }

    private void OnEnable()
    {
        scaleRoutine = StartCoroutine(ScaleLoop());
    }

    private void OnDisable()
    {
        if (scaleRoutine != null)
            StopCoroutine(scaleRoutine);

        transform.localScale = normalScale; // reset when disabled
    }

    private void Update()
    {
        sr.color = isOccupied ? redColor : greenColor;
    }

    IEnumerator ScaleLoop()
    {
        Vector3 big = normalScale * 1.2f;
        Vector3 small = normalScale * 0.8f;

        while (true)
        {
            yield return ScaleTo(big, 1f);
            yield return ScaleTo(normalScale, 1f);
            yield return ScaleTo(small, 1f);
            yield return ScaleTo(normalScale, 1f);
        }
    }

    IEnumerator ScaleTo(Vector3 target, float duration)
    {
        Vector3 start = transform.localScale;
        float time = 0f;

        while (time < duration)
        {
            transform.localScale = Vector3.Lerp(start, target, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        transform.localScale = target;
    }
}