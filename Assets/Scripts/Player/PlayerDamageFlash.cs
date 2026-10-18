using System.Collections;
using UnityEngine;

public class PlayerDamageFlash : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    [SerializeField] private Health health;
    [SerializeField] private SpriteRenderer spriteRenderer;

    private float flashInterval;
    private Color flashColor;
    private Coroutine routine;

    private void Awake()
    {
        flashInterval = playerData.GetFlashInterval();
        flashColor = playerData.GetFlashColor();
    }

    private void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;

        if (routine != null) StopCoroutine(routine);

        SetFlash(false);
    }

    // Parpadea con cualquier golpe, venga de donde venga
    private void HandleDamaged(DamageInfo info)
    {
        if (routine != null) StopCoroutine(routine);

        routine = StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (health.IsInvulnerable)
        {
            SetFlash(true);
            yield return new WaitForSeconds(flashInterval);

            SetFlash(false);
            yield return new WaitForSeconds(flashInterval);
        }

        SetFlash(false);
        routine = null;
    }

    private void SetFlash(bool on)
    {
        spriteRenderer.color = on ? flashColor : Color.white;
    }
}