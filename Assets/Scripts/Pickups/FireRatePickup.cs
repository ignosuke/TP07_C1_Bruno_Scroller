using UnityEngine;

public class FireRatePickup : Pickup
{
    [SerializeField] private FireRatePickupSo pickupData;

    protected override bool TryApply(GameObject player)
    {
        if (!player.TryGetComponent(out PlayerShooter shooter)) return false;

        shooter.ApplyFireRateBoost(pickupData.GetBoostedFireRate(), pickupData.GetDuration());
        return true;
    }
}
