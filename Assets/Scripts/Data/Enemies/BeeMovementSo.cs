using UnityEngine;

[CreateAssetMenu(fileName = "BeeMovementSo", menuName = "Data/Enemies/Bee Movement Data")]
public class BeeMovementSo : ScriptableObject
{
    [Header("Arc")]
    [SerializeField, Min(0.1f)] private float arcWidth = 2f;      // Medio ancho de la U
    [SerializeField, Min(0f)] private float arcDepth = 1f;        // Cuanto suben los extremos sobre el centro
    [SerializeField, Min(0.1f)] private float arcFrequency = 1f;  // Que tan rapido recorre la curva
    public float GetArcWidth() => arcWidth;
    public float GetArcDepth() => arcDepth;
    public float GetArcFrequency() => arcFrequency;

    [Header("Variation")]
    [SerializeField] private bool randomizeStartPhase = true; // Desincroniza las abejas entre si
    public bool GetRandomizeStartPhase() => randomizeStartPhase;
}