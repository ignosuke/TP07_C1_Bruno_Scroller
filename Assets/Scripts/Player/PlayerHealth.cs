using System;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private PlayerSo playerData;

    private bool hasShield;

    public bool HasShield => hasShield;

    public event Action OnShieldChanged;
    public event Action OnRevived;

    public override int MaxHealth => playerData.GetMaxHealth();
    protected override LayerMask DamageLayers => playerData.GetDamageLayers();
    protected override LayerMask UnavoidableDamageLayers => playerData.GetUnavoidableDamageLayers();
    protected override float InvulnerabilityTime => playerData.GetInvulnerabilityTime();
    
    protected override bool TryAbsorbDamage(int amount)
    {
        if (!hasShield) return false;

        SetShield(false);
        return true;
    }

    // Devuelve false si ya tenia escudo, asi el pickable no se consume al pedo
    public bool TryAddShield()
    {
        if (hasShield) return false;

        SetShield(true);
        return true;
    }

    private void SetShield(bool value)
    {
        hasShield = value;
        OnShieldChanged?.Invoke();
    }

    // El jugador no se destruye: el PlayerDeathHandler apaga sus componentes
    protected override void Die() { }

    public void Revive()
    {
        SetShield(false);
        Heal(MaxHealth);
        OnRevived?.Invoke();
    }
}