using UnityEngine;

public class HeartPickup : Pickup
{
    [SerializeField] private HeartPickupSo pickupData;

    protected override bool TryApply(GameObject player)
    {
        if (!player.TryGetComponent(out PlayerHealth health)) return false;

        if (health.CurrentHealth >= health.MaxHealth) return false; // Vida llena: no se gasta

        health.Heal(pickupData.GetHealAmount());
        return true;
    }
}
