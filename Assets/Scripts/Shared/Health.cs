using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    private int currentHealth;
    private float invulnerabilityCounter;

    public int CurrentHealth => currentHealth;
    public bool IsDead => currentHealth <= 0;
    public bool IsInvulnerable => invulnerabilityCounter > 0f;

    public event Action<int, int> OnHealthChanged; // actual, maximo
    public event Action<DamageInfo> OnDamaged;
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

        int sourceLayer = other.gameObject.layer;

        if (!DamageLayers.Contains(sourceLayer)) return;

        if (!other.TryGetComponent(out IDamageSource source)) return;

        TakeDamage(new DamageInfo(source.Damage, ResolveDirection(other.transform.position), sourceLayer));
    }

    // Daño directo sin fuente para debug
    public void TakeDamage(int amount)
    {
        TakeDamage(new DamageInfo(amount, Vector2.zero, gameObject.layer));
    }

    public void TakeDamage(DamageInfo info)
    {
        if (IsDead || info.Amount <= 0) return;

        invulnerabilityCounter = InvulnerabilityTime;

        // Si algo absorbe el golpe (escudo, armadura) la vida no se toca
        if (TryAbsorbDamage(info.Amount))
        {
            OnDamaged?.Invoke(info);
            return;
        }

        currentHealth = Mathf.Max(currentHealth - info.Amount, 0);

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        OnDamaged?.Invoke(info);

        if (IsDead)
        {
            OnDied?.Invoke();
            Die();
        }
    }

    private Vector2 ResolveDirection(Vector3 sourcePosition)
    {
        Vector2 direction = transform.position - sourcePosition;

        if (direction.sqrMagnitude < 0.0001f) return Vector2.zero;

        return direction.normalized;
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Min(currentHealth + amount, MaxHealth);
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }

    // Por defecto nadie absorbe nada. PlayerHealth lo usa para el escudo
    protected virtual bool TryAbsorbDamage(int amount) => false;

    protected abstract void Die();
}