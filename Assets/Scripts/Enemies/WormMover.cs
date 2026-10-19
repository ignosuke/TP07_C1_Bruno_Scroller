using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class WormMover : MonoBehaviour
{
    // Los gusanos viajan en linea recta, se detienen ante un muro o caida y frenan antes de girar
    [SerializeField] private WormMovementSo movementData;
    [SerializeField] private Transform frontCheck;      // Adelante, a la altura de los pies
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float moveSpeed;
    private float edgeCheckDistance;
    private float wallCheckDistance;
    private float turnPauseDuration;
    private LayerMask groundLayer;

    private Rigidbody2D rb;
    private int direction = 1; // 1 derecha, -1 izquierda
    private float pauseCounter;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        moveSpeed = movementData.GetMoveSpeed();
        edgeCheckDistance = movementData.GetEdgeCheckDistance();
        wallCheckDistance = movementData.GetWallCheckDistance();
        turnPauseDuration = movementData.GetTurnPauseDuration();
        groundLayer = movementData.GetGroundLayer();

        if (movementData.GetRandomizeStartDirection())
            SetDirection(Random.value < 0.5f ? 1 : -1);
    }

    private void FixedUpdate()
    {
        if (pauseCounter > 0f)
        {
            Wait();
            return;
        }

        if (IsBlocked())
        {
            pauseCounter = turnPauseDuration;
            Stop();
            return;
        }

        SetHorizontalSpeed(moveSpeed);
    }

    // Se queda quieto el tiempo de la pausa y recien al final gira
    private void Wait()
    {
        Stop();

        pauseCounter -= Time.fixedDeltaTime;

        if (pauseCounter > 0f) return;

        Flip();
    }

    // Se le acaba el piso o tiene una pared enfrente
    private bool IsBlocked()
    {
        return !HasGroundAhead() || HasWallAhead();
    }

    private bool HasGroundAhead()
    {
        return Physics2D.Raycast(frontCheck.position, Vector2.down, edgeCheckDistance, groundLayer);
    }

    private bool HasWallAhead()
    {
        return Physics2D.Raycast(frontCheck.position, Vector2.right * direction, wallCheckDistance, groundLayer);
    }

    private void SetHorizontalSpeed(float speed)
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    private void Stop()
    {
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
    }

    private void Flip() => SetDirection(-direction);

    private void SetDirection(int value)
    {
        direction = value;

        spriteRenderer.flipX = direction < 0;

        // Espejamos el punto de chequeo en vez de rotar, asi la barra de vida no se da vuelta
        Vector3 local = frontCheck.localPosition;
        local.x = Mathf.Abs(local.x) * direction;
        frontCheck.localPosition = local;
    }

    private void OnDrawGizmosSelected()
    {
        if (movementData == null || frontCheck == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(frontCheck.position,
            frontCheck.position + Vector3.down * movementData.GetEdgeCheckDistance());

        Gizmos.color = Color.magenta;
        Gizmos.DrawLine(frontCheck.position,
            frontCheck.position + Vector3.right * direction * movementData.GetWallCheckDistance());
    }
}