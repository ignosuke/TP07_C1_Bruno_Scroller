using System.Collections;
using UnityEngine;

// Sacude el pickup cada tanto para que se note. Va en el objeto visual, no en el collider.
public class PickupShake : MonoBehaviour
{
    [SerializeField] private PickupShakeSo shakeData;

    private float maxAngle;
    private float speed;
    private float duration;
    private float restDuration;

    private void Awake()
    {
        maxAngle = shakeData.GetMaxAngle();
        speed = shakeData.GetSpeed();
        duration = shakeData.GetDuration();
        restDuration = shakeData.GetRestDuration();
    }

    private void OnEnable()
    {
        StartCoroutine(Loop());
    }

    private IEnumerator Loop()
    {
        while (true)
        {
            yield return Shake();
            yield return new WaitForSeconds(restDuration);
        }
    }

    // Seno multiplicado por un decaimiento lineal: oscila y se va frenando solo
    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float decay = 1f - elapsed / duration;
            float angle = Mathf.Sin(elapsed * speed) * maxAngle * decay;

            transform.localRotation = Quaternion.Euler(0f, 0f, angle);

            yield return null;
        }

        transform.localRotation = Quaternion.identity;
    }
}