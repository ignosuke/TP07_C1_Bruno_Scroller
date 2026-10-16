using System;
using UnityEngine;

public abstract class Health : MonoBehaviour
{
    private int currentHealth;
    private float invulnerabilityCounter;

    public bool IsDead => currentHealth <= 0;
    public bool IsInvulnerable => invulnerabilityCounter > 0f;

    public event Action<int, int> OnHealthChanged; // actual, maximo
    public event Action OnDamaged;
    public event Action OnDied;

    protected abstract int MaxHealth { get; }
    protected abstract LayerMask DamageLayers { get; }
    protected virtual float InvulnerabilityTime => 0f;

    protected virtual void Awake()
    {
        currentHealth = MaxHealth;
    }

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

        TakeDamage(source.Damage);
    }

    private bool IsInDamageLayers(int layer)
    {
        return (DamageLayers.value & (1 << layer)) != 0;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Max(currentHealth - amount, 0);
        invulnerabilityCounter = InvulnerabilityTime;

        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
        OnDamaged?.Invoke();

        if (IsDead)
        {
            OnDied?.Invoke();
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (IsDead || amount <= 0) return;

        currentHealth = Mathf.Min(currentHealth + amount, MaxHealth);
        OnHealthChanged?.Invoke(currentHealth, MaxHealth);
    }

    protected abstract void Die();
}