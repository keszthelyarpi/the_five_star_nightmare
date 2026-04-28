using System.Collections;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
    [Header("Forgatás beállításai")]
    public float rotationAngle = -180f;
    public float speed = 1f;
    public Transform targetTransform;

    public void StartRotation()
    {
        StartCoroutine(RotateSmoothly());
    }

    private IEnumerator RotateSmoothly()
    {
        Transform target = targetTransform != null ? targetTransform : transform;
        Quaternion startRotation = target.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, 0, rotationAngle);
        float timeElapsed = 0;

        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * speed;
            target.rotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed);
            yield return null;
        }
        target.rotation = endRotation;
    }
}