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
    private LayerMask knockbackLayers;

    private Rigidbody2D rb;
    private Coroutine routine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        horizontalForce = playerData.GetKnockbackHorizontalForce();
        verticalForce = playerData.GetKnockbackVerticalForce();
        duration = playerData.GetKnockbackDuration();
        knockbackLayers = playerData.GetKnockbackLayers();
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

    private void HandleDamaged(DamageInfo info)
    {
        if (!info.IsFrom(knockbackLayers)) return; // El vacio y los peligros no empujan
        if (info.Direction == Vector2.zero) return;

        if (routine != null) StopCoroutine(routine);

        routine = StartCoroutine(Push(info.Direction));
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