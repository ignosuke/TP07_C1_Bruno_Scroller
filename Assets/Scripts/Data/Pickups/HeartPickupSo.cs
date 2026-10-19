using UnityEngine;

[CreateAssetMenu(fileName = "HeartPickupSo", menuName = "Data/Pickups/Heart Pickup Data So")]
public class HeartPickupSo : ScriptableObject
{
    [SerializeField, Min(1)] private int healAmount = 1;
    public int GetHealAmount() => healAmount;
}