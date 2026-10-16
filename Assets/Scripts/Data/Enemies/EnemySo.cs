using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemies/Enemy Data")]
public class EnemySo : ScriptableObject
{
    [Header("Stats")]
    [SerializeField, Min(1)] private int maxHealth = 3;
    [SerializeField, Min(1)] private int contactDamage = 1;
    public int GetMaxHealth() => maxHealth;
    public int GetContactDamage() => contactDamage;

    [Header("Movement")]
    [SerializeField, Min(0f)] private float moveSpeed = 2f;
    [SerializeField, Min(0.05f)] private float edgeCheckDistance = 0.6f;  // Hacia abajo: busca piso
    [SerializeField, Min(0.05f)] private float wallCheckDistance = 0.3f;  // Hacia adelante: busca pared
    public float GetMoveSpeed() => moveSpeed;
    public float GetEdgeCheckDistance() => edgeCheckDistance;
    public float GetWallCheckDistance() => wallCheckDistance;

    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask damageLayers;   // Capas que dañan al enemigo
    public LayerMask GetGroundLayer() => groundLayer;
    public LayerMask GetDamageLayers() => damageLayers;
}
