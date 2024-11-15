using UnityEngine;
using System.Collections;

public class LerpYAxis : MonoBehaviour
{
    public float startY = 0f;
    public float endY = 10f;
    public float duration = 5f;

    public void StartLerp()
    {
        StartCoroutine(LerpYPosition(startY, endY, duration));
    }

    private IEnumerator LerpYPosition(float start, float end, float time)
    {
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            float t = elapsedTime / time;
            float newY = Mathf.Lerp(start, end, t);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = new Vector3(transform.position.x, end, transform.position.z);
    }
}
