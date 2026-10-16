using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJumper : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    private InputActionReference jumpAction;

    private float jumpForce;
    private float remainingForceOnTapJump; // Cuanto de la velocidad sobrevive al soltar

    private float riseGravity;          // Mientras sube
    private float fallGravity;          // Apenas empieza a caer
    private float heavyFallGravity;     // Caida acelerada
    private float heavyFallThreshold;   // Velocidad de caida a la que se activa

    private float jumpBufferTime;
    private float jumpLockTime;
    private float coyoteTime;

    private float bufferCounter;
    private float lockCounter;
    private float coyoteCounter;

    private PlayerGroundChecker groundChecker;
    private Rigidbody2D rb;
    private bool jumpRequested;
    private bool jumpCutRequested;

    public event Action OnJumped;

    private void Awake()
    {
        jumpAction = playerData.GetJumpAction();

        jumpForce = playerData.GetJumpForce();
        remainingForceOnTapJump = playerData.GetRemainingForceOnTapJump();

        riseGravity = playerData.GetRiseGravity();
        fallGravity = playerData.GetFallGravity();
        heavyFallGravity = playerData.GetHeavyFallGravity();
        heavyFallThreshold = playerData.GetHeavyFallThreshold();

        coyoteTime = playerData.GetCoyoteTime();
        jumpBufferTime = playerData.GetJumpBufferTime();
        jumpLockTime = playerData.GetJumpLockTime();

        if (groundChecker == null) groundChecker = GetComponentInChildren<PlayerGroundChecker>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        jumpAction.action.performed += OnJumpPerformed;
        jumpAction.action.canceled += OnJumpCanceled;
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        jumpAction.action.performed -= OnJumpPerformed;
        jumpAction.action.canceled -= OnJumpCanceled;
        jumpAction.action.Disable();

        ResetCounters();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        jumpRequested = true;
    }

    private void OnJumpCanceled(InputAction.CallbackContext context)
    {
        jumpCutRequested = true;
    }

    private void Update()
    {
        if (lockCounter >= 0f) lockCounter -= Time.deltaTime;

        UpdateCoyote();
        UpdateBuffer();
    }

    private void FixedUpdate()
    {
        ApplyGravity();
        HandleTapJump();
        HandleJump();
    }

    private void HandleJump()
    {
        if (bufferCounter <= 0f || coyoteCounter <= 0f) return;

        Jump();
    }

    private void Jump()
    {
        bufferCounter = 0f;
        coyoteCounter = 0f;             // Consumimos el coyote para que no entren dos saltos seguidos
        lockCounter = jumpLockTime;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // Arranca siempre desde cero
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        OnJumped?.Invoke();
    }

    // Al soltar la tecla durante la subida se corta parte de la velocidad: tap = salto bajo, hold = salto completo
    private void HandleTapJump()
    {
        if (!jumpCutRequested) return;

        jumpCutRequested = false;

        if (rb.linearVelocity.y > 0f)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * remainingForceOnTapJump);
    }

    // Gravedad personalizada, primero sube liviano, acelera al bajar y cuando la caida pasa cierta velocidad acelera de golpe
    private void ApplyGravity()
    {
        float verticalSpeed = rb.linearVelocity.y;

        if (verticalSpeed > 0f)
            rb.gravityScale = riseGravity;
        else if (verticalSpeed > -heavyFallThreshold)
            rb.gravityScale = fallGravity;
        else
            rb.gravityScale = heavyFallGravity;
    }

    // El coyote se recarga pisando suelo, salvo en los primeros frames despues de saltar
    private void UpdateCoyote()
    {
        if (groundChecker.GetIsGrounded() && lockCounter <= 0f)
            coyoteCounter = coyoteTime;
        else
            coyoteCounter -= Time.deltaTime;
    }

    // El buffer guarda el input unos ms por si todavia no podemos saltar
    private void UpdateBuffer()
    {
        if (jumpRequested)
        {
            jumpRequested = false;
            bufferCounter = jumpBufferTime;
            return;
        }

        bufferCounter -= Time.deltaTime;
    }

    private void ResetCounters()
    {
        jumpRequested = false;
        jumpCutRequested = false;
        coyoteCounter = 0f;
        bufferCounter = 0f;
        lockCounter = 0f;
    }
}