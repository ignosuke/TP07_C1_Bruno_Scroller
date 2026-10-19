using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Player", menuName = "Data/Player/Player Data")]
public class PlayerSo : ScriptableObject
{
    [Header("Input Actions")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;
    [SerializeField] private InputActionReference shootAction;
    public InputActionReference GetMoveAction() => moveAction;
    public InputActionReference GetJumpAction() => jumpAction;
    public InputActionReference GetShootAction() => shootAction;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6.0f;
    public float GetMoveSpeed() => moveSpeed;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 16.0f;
    [SerializeField] private float remainingForceOnTapJump = .4f; // Cuanto de la velocidad sobrevive al soltar
    public float GetJumpForce() => jumpForce;
    public float GetRemainingForceOnTapJump() => remainingForceOnTapJump;

    [Header("Gravity")] // Gravedad custom para logica de salto personalizada
    [SerializeField] private float riseGravity = 5f;      // Mientras sube
    [SerializeField] private float fallGravity = 9f;      // Apenas empieza a caer
    [SerializeField] private float heavyFallGravity = 13f; // Caida acelerada
    [SerializeField] private float heavyFallThreshold = 14f; // Velocidad de caida a la que se activa
    public float GetRiseGravity() => riseGravity;
    public float GetFallGravity() => fallGravity;
    public float GetHeavyFallGravity() => heavyFallGravity;
    public float GetHeavyFallThreshold() => heavyFallThreshold;

    [Header("Ground Checker")]
    [SerializeField] private float feetWidth = 0.3f;  // Ancho del personaje a la altura de los pies
    [SerializeField] private float checkDistance = 0.2f; // Distancia del raycast para detectar el suelo
    [SerializeField] private LayerMask groundLayer;
    public float GetFeetWidth() => feetWidth;
    public float GetCheckDistance() => checkDistance;
    public LayerMask GetGroundLayer() => groundLayer;

    [Header("Jump Feel")]
    [SerializeField] private float jumpBufferTime = 0.1f; // Tiempo que se puede presionar el boton antes de aterrizar y aun asi saltar
    [SerializeField] private float jumpLockTime = 0.08f; // Tiempo que se bloquea el salto despues de saltar, para evitar un doble salto accidental
    [SerializeField] private float coyoteTime = 0.08f; // Tiempo que se puede saltar despues de salir del suelo
    public float GetJumpBufferTime() => jumpBufferTime;
    public float GetJumpLockTime() => jumpLockTime;
    public float GetCoyoteTime() => coyoteTime;

    [Header("Shooting")]
    [SerializeField, Range(0.1f, 20f)] private float fireRate = 2f; // Disparo por segundos
    public float GetFireRate() => fireRate;

    [Header("Health")]
    [SerializeField, Min(1)] private int maxHealth = 3;
    [SerializeField, Min(0f)] private float invulnerabilityTime = 1f;
    public int GetMaxHealth() => maxHealth;
    public float GetInvulnerabilityTime() => invulnerabilityTime;

    [Header("Damage Layers")]
    [SerializeField] private LayerMask damageLayers;                 // Lethal + Enemies
    [SerializeField] private LayerMask unavoidableDamageLayers;      // Lethal
    [SerializeField] private LayerMask knockbackLayers;              // Enemies
    [SerializeField] private LayerMask respawnLayers;                // Lethal
    public LayerMask GetDamageLayers() => damageLayers;
    public LayerMask GetUnavoidableDamageLayers() => unavoidableDamageLayers;
    public LayerMask GetKnockbackLayers() => knockbackLayers;
    public LayerMask GetRespawnLayers() => respawnLayers;

    [Header("Damage Feedback")]
    [SerializeField, Min(0f)] private float knockbackHorizontalForce = 6f;
    [SerializeField, Min(0f)] private float knockbackVerticalForce = 4f;
    [SerializeField, Min(0f)] private float knockbackDuration = 0.25f;
    [SerializeField] private Color flashColor = new Color(1f, 1f, 1f, 0.3f);
    [SerializeField, Min(0.01f)] private float flashInterval = 0.08f;
    [SerializeField, Min(0.05f)] private float safePointInterval = 0.25f;
    public float GetKnockbackHorizontalForce() => knockbackHorizontalForce;
    public float GetKnockbackVerticalForce() => knockbackVerticalForce;
    public float GetKnockbackDuration() => knockbackDuration;
    public Color GetFlashColor() => flashColor;
    public float GetFlashInterval() => flashInterval;
    public float GetSafePointInterval() => safePointInterval;

    [Header("Animation")]
    [SerializeField] private float shootAnimationTime = 0.2f; // Tiempo de la animacion al disparar (no confundir con la cadencia)
    public float GetShootAnimationTime() => shootAnimationTime;
}
