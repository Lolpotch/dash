using System.Collections;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] float multiplier = 0f;
    [SerializeField] float shakeDelay = 0f;
    [SerializeField] int shakeAmount = 0;

    public IEnumerator CameraShake()
    {
        for (int i = 0; i < shakeAmount; i++)
        {
            float randomX, randomY;
            randomX = Random.Range(-1f, 1f) * multiplier;
            randomY = Random.Range(-1f, 1f) * multiplier;

            Vector2 newPos = new Vector3(randomX, randomY, 0f);
            transform.localPosition = newPos;
            
            yield return new WaitForSeconds(shakeDelay);
        }
        transform.localPosition = Vector3.zero;
    }

    public IEnumerator CameraShake(float multiplier, float shakeSpeed, int shakeAmount)
    {
        for (int i = 0; i < shakeAmount; i++)
        {
            float randomX, randomY;
            randomX = Random.Range(-1f, 1f) * multiplier;
            randomY = Random.Range(-1f, 1f) * multiplier;

            Vector2 newPos = new Vector3(randomX, randomY, 0f);
            transform.localPosition = newPos;

            yield return new WaitForSeconds(shakeSpeed);
        }
        transform.localPosition = Vector3.zero;
    }
}
