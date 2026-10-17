using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    private int currentHealth;
    private float invulnerabilityCounter;

    public int CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0;
    public bool IsInvulnerable => invulnerabilityCounter > 0f;

    public event Action<int, int> OnHealthChanged;  // actual, maximo
    public event Action<Vector2> OnDamaged;         // direccion del golpe, desde la fuente hacia nosotros
    public event Action OnDied;

    public abstract int MaxHealth { get; }
    protected abstract LayerMask DamageLayers { get; }
    protected virtual float InvulnerabilityTime => 0f;

    protected virtual void Awake()
    {
        currentHealth = MaxHealth;
    }

    // En Start para que las barras y el HUD ya esten suscriptos cuando llega el primer valor
    protected virtual void Start()
    {
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }

    protected virtual void Update()
    {
        invulnerabilityCounter -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryTakeDamageFrom(collision.collider);
    private void OnCollisionStay2D(Collision2D collision) => TryTakeDamageFrom(collision.collider);
    private void OnTriggerEnter2D(Collider2D other) => TryTakeDamageFrom(other);
    private void OnTriggerStay2D(Collider2D other) => TryTakeDamageFrom(other);

    private void TryTakeDamageFrom(Collider2D other)
    {
        if (IsDead || IsInvulnerable) return;

        if (!IsInDamageLayers(other.gameObject.layer)) return;

        if (!other.TryGetComponent(out IDamageSource source)) return;

        TakeDamage(source.Damage, other.transform.position);
    }

    private bool IsInDamageLayers(int layer)
    {
        return (DamageLayers.value & (1 << layer)) != 0;
    }

    // Sin origen: daño sin empuje (caida al vacio, veneno, etc.)
    public void TakeDamage(int amount) => TakeDamage(amount, transform.position);

    public void TakeDamage(int amount, Vector3 sourcePosition)
    {
        if (IsDead || amount <= 0) return;

        invulnerabilityCounter = InvulnerabilityTime;

        Vector2 direction = ResolveDirection(sourcePosition);

        // Si algo absorbe el golpe (escudo, armadura) la vida no se toca
        if (TryAbsorbDamage(amount))
        {
            OnDamaged?.Invoke(direction);
            return;
        }

        currentHealth = Mathf.Max(currentHealth - amount, 0);

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        OnDamaged?.Invoke(direction);

        if (IsDead)
        {
            OnDied?.Invoke();
            Die();
        }
    }

    private Vector2 ResolveDirection(Vector3 sourcePosition)
    {
        Vector2 direction = transform.position - sourcePosition;

        if (direction.sqrMagnitude < 0.0001f) return Vector2.up; // Mismo punto: empuje neutro

        return direction.normalized;
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Min(currentHealth + amount, MaxHealth);
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }

    // Por defecto nadie absorbe nada. PlayerHealth lo usa para el escudo.
    protected virtual bool TryAbsorbDamage(int amount) => false;

    protected abstract void Die();
}