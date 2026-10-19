using UnityEngine;

[CreateAssetMenu(fileName = "PickupShakeSo", menuName = "Data/Pickups/Shake Data")]
public class PickupShakeSo : ScriptableObject
{
    [SerializeField, Min(0f)] private float maxAngle = 20f;      // Cuanto rota en Z
    [SerializeField, Min(0.1f)] private float speed = 14f;       // Que tan rapido va y viene
    [SerializeField, Min(0.1f)] private float duration = 0.8f;   // Cuanto dura la sacudida
    [SerializeField, Min(0f)] private float restDuration = 3.5f; // Pausa hasta la siguiente
    public float GetMaxAngle() => maxAngle;
    public float GetSpeed() => speed;
    public float GetDuration() => duration;
    public float GetRestDuration() => restDuration;
}