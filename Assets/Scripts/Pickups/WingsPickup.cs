using UnityEngine;

public class WingsPickup : Pickup
{
    [SerializeField] private LevelGoal levelGoal;

    protected override bool TryApply(GameObject player)
    {
        if (!player.TryGetComponent(out PlayerHealth _)) return false;

        levelGoal.CompleteLevel();
        return true;
    }
}