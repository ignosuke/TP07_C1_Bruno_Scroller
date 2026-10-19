using UnityEngine;

[CreateAssetMenu(fileName = "FlyMovementSo", menuName = "Data/Enemies/Fly Movement Data")]
public class FlyMovementSo : ScriptableObject
{
    [Header("Hover")]
    [SerializeField, Min(0f)] private float hoverAmplitude = 0.5f;   // Cuanto sube y baja
    [SerializeField, Min(0.1f)] private float hoverFrequency = 1.5f; // Que tan rapido oscila
    public float GetHoverAmplitude() => hoverAmplitude;
    public float GetHoverFrequency() => hoverFrequency;

    [Header("Variation")]
    [SerializeField] private bool randomizeStartPhase = true; // Desincroniza las moscas entre si
    public bool GetRandomizeStartPhase() => randomizeStartPhase;
}
