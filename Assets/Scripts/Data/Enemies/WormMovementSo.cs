using UnityEngine;

[CreateAssetMenu(fileName = "WormMovementSo", menuName = "Data/Enemies/Worm Movement Data")]
public class WormMovementSo : ScriptableObject
{
    [Header("Walk")]
    [SerializeField, Min(0f)] private float moveSpeed = 2f;
    [SerializeField, Min(0f)] private float turnPauseDuration = 1f; // Tiempo que tarda antes de girar
    public float GetMoveSpeed() => moveSpeed;
    public float GetTurnPauseDuration() => turnPauseDuration;

    [Header("Ground Check")]
    [SerializeField, Min(0.05f)] private float edgeCheckDistance = 0.6f;  // Hacia abajo: busca piso
    [SerializeField, Min(0.05f)] private float wallCheckDistance = 0.3f;  // Hacia adelante: busca pared
    [SerializeField] private LayerMask groundLayer;
    public float GetEdgeCheckDistance() => edgeCheckDistance;
    public float GetWallCheckDistance() => wallCheckDistance;
    public LayerMask GetGroundLayer() => groundLayer;

    [Header("Variation")]
    [SerializeField] private bool randomizeStartDirection = true;
    public bool GetRandomizeStartDirection() => randomizeStartDirection;
}