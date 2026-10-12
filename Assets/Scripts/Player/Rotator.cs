using UnityEngine;

public class Rotator : MonoBehaviour
{
    private const float MinSpeedToFlip = 0.01f; // Zona muerta: evita girar por residuos de velocidad

    private static readonly Quaternion FacingRight = Quaternion.identity;
    private static readonly Quaternion FacingLeft = Quaternion.Euler(0f, 180f, 0f);

    private Rigidbody2D rb;
    private bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float horizontalSpeed = rb.linearVelocity.x;

        if (Mathf.Abs(horizontalSpeed) < MinSpeedToFlip) return; // Si el movimiento es insignificante mantiene la ultima direccion

        bool shouldFaceRight = horizontalSpeed > 0f;

        if (shouldFaceRight == isFacingRight) return;

        isFacingRight = shouldFaceRight;
        transform.localRotation = isFacingRight ? FacingRight : FacingLeft;
    }
}