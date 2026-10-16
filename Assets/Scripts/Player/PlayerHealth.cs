using System;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private PlayerSo playerData;

    protected override int MaxHealth => playerData.GetMaxHealth();
    protected override LayerMask DamageLayers => playerData.GetDamageLayers();
    protected override float InvulnerabilityTime => playerData.GetInvulnerabilityTime();

    public event Action OnRevived;

    protected override void Die() { }

    public void Revive()
    {
        Heal(MaxHealth);
        OnRevived?.Invoke();
    }
}
