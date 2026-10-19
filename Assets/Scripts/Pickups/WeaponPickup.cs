using UnityEngine;

public class WeaponPickup : Pickup
{
    protected override bool TryApply(GameObject player)
    {
        if (!player.TryGetComponent(out PlayerShooter shooter)) return false;

        return shooter.TryArm();
    }
}

