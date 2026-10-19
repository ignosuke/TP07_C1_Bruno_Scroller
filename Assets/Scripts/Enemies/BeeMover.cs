using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BeeMover : MonoBehaviour
{
    // Las abejas se mueven haciendo una parabola en U
    [SerializeField] private BeeMovementSo movementData;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float arcWidth;
    private float arcDepth;
    private float arcFrequency;
    private float phaseOffset;

    private Rigidbody2D rb;
    private Vector2 origin; // El punto mas bajo de la U
    private float timer;
    private int direction = 1;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        arcWidth = movementData.GetArcWidth();
        arcDepth = movementData.GetArcDepth();
        arcFrequency = movementData.GetArcFrequency();

        phaseOffset = movementData.GetRandomizeStartPhase()
            ? Random.Range(0f, Mathf.PI * 2f)
            : 0f;

        // Muevo el origen para que arranque justo donde la dejaron en el editor
        origin = rb.position - GetOffsetAt(0f);
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;

        UpdateDirection(Mathf.Cos(timer * arcFrequency + phaseOffset));

        rb.MovePosition(origin + GetOffsetAt(timer));
    }

    private Vector2 GetOffsetAt(float time)
    {
        float wave = Mathf.Sin(time * arcFrequency + phaseOffset); // -1 a 1

        return new Vector2(wave * arcWidth, wave * wave * arcDepth);
    }

    // Matematica dificil
    // El coseno es la derivada del seno: su signo dice hacia donde va
    private void UpdateDirection(float horizontalVelocity)
    {
        int next = horizontalVelocity >= 0f ? 1 : -1;

        if (next == direction) return;

        direction = next;
        spriteRenderer.flipX = direction < 0;
    }

    // Para ver la parabola en el editor
    private void OnDrawGizmosSelected()
    {
        if (movementData == null) return;

        Vector3 center = Application.isPlaying ? (Vector3)origin : transform.position;
        float width = movementData.GetArcWidth();
        float depth = movementData.GetArcDepth();

        Gizmos.color = Color.yellow;

        Vector3 previous = center + new Vector3(-width, depth, 0f);

        for (int i = 1; i <= 20; i++)
        {
            float wave = Mathf.Lerp(-1f, 1f, i / 20f);
            Vector3 point = center + new Vector3(wave * width, wave * wave * depth, 0f);

            Gizmos.DrawLine(previous, point);
            previous = point;
        }
    }
}