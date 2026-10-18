using UnityEngine;

// Cualquier peligro del escenario: el vacio, pinchos, lava. Solo expone su daño.
public class Hazard : MonoBehaviour, IDamageSource
{
    [SerializeField] private HazardSo hazardData;

    public int Damage => hazardData.GetDamage();

    public void SetData(HazardSo data) => hazardData = data;
}