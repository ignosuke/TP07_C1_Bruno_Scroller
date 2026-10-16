using System;
using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;

    [SerializeField] private Transform origin;        // Punto a la altura de los pies
    private float feetWidth = 0.3f;  // Ancho del personaje a la altura de los pies
    private float checkDistance = 0.2f; // Distancia del raycast para detectar el suelo
    private LayerMask groundLayer;

    private bool IsGrounded;
    public bool GetIsGrounded() => IsGrounded;

    public event Action OnLanded;
    public event Action OnLeftGround;

    private void Awake()
    {
        feetWidth = playerData.GetFeetWidth();
        checkDistance = playerData.GetCheckDistance();
        groundLayer = playerData.GetGroundLayer();
    }

    private void Reset()
    {
        origin = transform;
    }

    private void FixedUpdate()
    {
        bool wasGrounded = IsGrounded;

        IsGrounded = CheckGround();

        if (IsGrounded && !wasGrounded)
            OnLanded?.Invoke();
        else if (!IsGrounded && wasGrounded)
            OnLeftGround?.Invoke();
    }

    // Tres rayos: centro y ambos extremos, para no perder el salto parado en el borde de una plataforma
    private bool CheckGround()
    {
        float halfWidth = feetWidth * 0.5f;
        Vector2 center = origin.position;

        return CastRay(center)
            || CastRay(center + Vector2.left * halfWidth)
            || CastRay(center + Vector2.right * halfWidth);
    }

    private bool CastRay(Vector2 rayOrigin)
    {
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down, checkDistance, groundLayer);

        if (!hit) return false;

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        if (origin == null) return;

        float halfWidth = feetWidth * 0.5f;
        Vector2 center = origin.position;

        Gizmos.color = Application.isPlaying && IsGrounded ? Color.green : Color.red;

        DrawRayGizmo(center);
        DrawRayGizmo(center + Vector2.left * halfWidth);
        DrawRayGizmo(center + Vector2.right * halfWidth);
    }

    private void DrawRayGizmo(Vector2 rayOrigin)
    {
        Gizmos.DrawLine(rayOrigin, rayOrigin + Vector2.down * checkDistance);
    }
}