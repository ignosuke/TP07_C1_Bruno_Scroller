using UnityEngine;

public class BlueHeartPickup : Pickup
{
    protected override bool TryApply(GameObject player)
    {
        if (!player.TryGetComponent(out PlayerHealth health)) return false;

        return health.TryAddShield(); // Devuelve false si tenia uno
    }
}