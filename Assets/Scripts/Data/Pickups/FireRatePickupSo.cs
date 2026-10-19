using UnityEngine;

[CreateAssetMenu(fileName = "FireRatePickupSo", menuName = "Data/Pickups/Fire Rate Pickup Data So")]
public class FireRatePickupSo : ScriptableObject
{
    [SerializeField, Range(0.1f, 30f)] private float boostedFireRate = 10f; // Disparos por segundo
    [SerializeField, Min(0.1f)] private float duration = 6f;
    public float GetBoostedFireRate() => boostedFireRate;
    public float GetDuration() => duration;
}
