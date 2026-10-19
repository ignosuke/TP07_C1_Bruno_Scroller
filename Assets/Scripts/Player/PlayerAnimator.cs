using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimator : MonoBehaviour
{
    private const float MinSpeedToRun = 0.1f;
    private const int ArmedOffset = 3; // Distancia entre Idle/Run/Jump y su version con arma

    // Tienen que coincidir exactamente con los nombres de los estados del Animator Controller
    private static readonly string[] StateNames =
    {
        "Idle", "Run", "Jump", // Animaciones con variante desarmada
        "IdleArmed", "RunArmed", "JumpArmed", // Animaciones con variante armada
        "Shoot", "ShootRun", "ShootAir", // Disparo en suelo y aire
        "Death"
    };

    [SerializeField] private PlayerSo playerData;
    [SerializeField] private PlayerGroundChecker groundChecker;
    [SerializeField] private PlayerShooter shooter;
    [SerializeField] private PlayerHealth health;

    private Animator animator;
    private Rigidbody2D rb;

    private int[] stateHashes;
    private float shootAnimationTime;
    private float shootCounter;

    private bool isArmed = false;
    private bool isDead;

    private PlayerAnimationState currentState = PlayerAnimationState.Idle;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponentInParent<Rigidbody2D>();

        shootAnimationTime = playerData.GetShootAnimationTime();

        if (groundChecker == null)
            groundChecker = GetComponentInParent<PlayerGroundChecker>();

        CacheHashes();
    }

    private void OnEnable()
    {
        shooter.OnShot += PlayShoot;
        health.OnDied += PlayDeath;
        health.OnRevived += PlayRevive;
        shooter.OnArmedChanged += SetArmed;
    }

    private void OnDisable()
    {
        shooter.OnShot -= PlayShoot;
        health.OnDied -= PlayDeath;
        health.OnRevived -= PlayRevive;
    }

    private void Update()
    {
        if (isDead) return;

        shootCounter -= Time.deltaTime;

        SetState(ResolveState());
    }

    private void CacheHashes()
    {
        stateHashes = new int[StateNames.Length];

        for (int i = 0; i < StateNames.Length; i++)
            stateHashes[i] = Animator.StringToHash(StateNames[i]);
    }

    // Prioridad: disparo > aire > correr > quieto
    private PlayerAnimationState ResolveState()
    {
        bool isGrounded = groundChecker.GetIsGrounded();
        bool isMoving = Mathf.Abs(rb.linearVelocity.x) > MinSpeedToRun;

        if (shootCounter > 0f)
        {
            if (!isGrounded) return PlayerAnimationState.ShootAir;

            return isMoving ? PlayerAnimationState.ShootRun : PlayerAnimationState.Shoot;
        }

        if (!isGrounded)
            return WithWeapon(PlayerAnimationState.Jump);

        if (isMoving)
            return WithWeapon(PlayerAnimationState.Run);

        return WithWeapon(PlayerAnimationState.Idle);
    }

    private PlayerAnimationState WithWeapon(PlayerAnimationState baseState)
    {
        if (!isArmed) return baseState;

        return baseState + ArmedOffset;
    }

    private void SetState(PlayerAnimationState state)
    {
        if (state == currentState) return;

        currentState = state;
        animator.Play(stateHashes[(int)state]);
    }

    // Al jugador pickear el arma
    public void SetArmed(bool value)
    {
        isArmed = value;
    }

    // Seteado por el script de Shooter
    public void PlayShoot()
    {
        shootCounter = shootAnimationTime;
    }

    public void PlayDeath()
    {
        isDead = true;
        SetState(PlayerAnimationState.Death);
    }

    public void PlayRevive()
    {
        isDead = false;
        shootCounter = 0f;
        SetState(PlayerAnimationState.Idle);
    }
}