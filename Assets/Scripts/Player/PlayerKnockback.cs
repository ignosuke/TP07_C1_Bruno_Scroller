using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerKnockback : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    [SerializeField] private Health health;
    [SerializeField] private MonoBehaviour[] disableWhileKnocked; // Mover: sino pisa la velocidad

    private float horizontalForce;
    private float verticalForce;
    private float duration;

    private Rigidbody2D rb;
    private Coroutine routine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        horizontalForce = playerData.GetKnockbackHorizontalForce();
        verticalForce = playerData.GetKnockbackVerticalForce();
        duration = playerData.GetKnockbackDuration();
    }

    private void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;

        if (routine != null)
        {
            StopCoroutine(routine);
            routine = null;
        }

        RestoreControl();
    }

    private void HandleDamaged(Vector2 direction)
    {
        if (routine != null) StopCoroutine(routine);

        routine = StartCoroutine(Push(direction));
    }

    private IEnumerator Push(Vector2 direction)
    {
        SetControlEnabled(false);

        float horizontal = direction.x >= 0f ? 1f : -1f;

        rb.linearVelocity = Vector2.zero;
        rb.AddForce(new Vector2(horizontal * horizontalForce, verticalForce), ForceMode2D.Impulse);

        yield return new WaitForSeconds(duration);

        routine = null;
        RestoreControl();
    }

    // Nunca devolvemos el control si el jugador murio: de eso se encarga el PlayerDeathHandler
    private void RestoreControl()
    {
        if (health.IsDead) return;

        SetControlEnabled(true);
    }

    private void SetControlEnabled(bool value)
    {
        foreach (MonoBehaviour component in disableWhileKnocked)
            component.enabled = value;
    }
}