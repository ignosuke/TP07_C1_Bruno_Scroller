using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlyMover : MonoBehaviour
{
    // Las moscas suben y bajan en el lugar porque estan gordas
    [SerializeField] private FlyMovementSo movementData;

    private float amplitude;
    private float frequency;
    private float phaseOffset;

    private Rigidbody2D rb;
    private Vector2 origin; // Centro de la oscilacion
    private float timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        amplitude = movementData.GetHoverAmplitude();
        frequency = movementData.GetHoverFrequency();

        phaseOffset = movementData.GetRandomizeStartPhase()
            ? Random.Range(0f, Mathf.PI * 2f)
            : 0f;

        // Corremos el centro para que arranque justo donde la dejaron en el editor
        origin = rb.position - new Vector2(0f, GetOffsetAt(0f));
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        rb.MovePosition(origin + new Vector2(0f, GetOffsetAt(timer)));
    }

    private float GetOffsetAt(float time)
    {
        return Mathf.Sin(time * frequency + phaseOffset) * amplitude;
    }

    private void OnDrawGizmosSelected()
    {
        if (movementData == null) return;

        Vector3 center = Application.isPlaying ? (Vector3)origin : transform.position;
        float range = movementData.GetHoverAmplitude();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(center + Vector3.down * range, center + Vector3.up * range);
    }
}