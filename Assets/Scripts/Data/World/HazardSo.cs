using UnityEngine;

[CreateAssetMenu(fileName = "Hazard", menuName = "Data/Hazards/Hazard Data")]
public class HazardSo : ScriptableObject
{
    [SerializeField, Min(1)] private int damage = 1;
    [SerializeField] private LayerMask hazardLayer;
    public int GetDamage() => damage;
    public LayerMask GetHazardLayer() => hazardLayer;
}