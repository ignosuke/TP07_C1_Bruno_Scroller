using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private MonoBehaviour[] disableOnDeath;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        health.OnDied += HandleDeath;
        health.OnRevived += HandleRevive;
    }

    private void OnDisable()
    {
        health.OnDied -= HandleDeath;
        health.OnRevived -= HandleRevive;
    }

    private void HandleDeath()
    {
        rb.linearVelocity = Vector2.zero;
        SetComponentsEnabled(false);
    }

    private void HandleRevive() => SetComponentsEnabled(true);

    private void SetComponentsEnabled(bool value)
    {
        foreach (MonoBehaviour component in disableOnDeath)
            component.enabled = value;
    }
}
