using UnityEngine;

[CreateAssetMenu(fileName = "Camera", menuName = "Data/Camera/Camera Data")]
public class CameraSo : ScriptableObject
{
    [Header("Dead Zone")]
    [SerializeField, Min(0f)] private float deadZoneWidth = 4f;   // En unidades de mundo
    [SerializeField, Min(0f)] private float deadZoneHeight = 3f;

    [Header("Follow")]
    [SerializeField, Min(0.01f)] private float smoothTime = 0.2f; // Cuanto tarda en alcanzar al jugador
    [SerializeField] private Vector2 offset = Vector2.zero;      

    public float GetDeadZoneWidth() => deadZoneWidth;
    public float GetDeadZoneHeight() => deadZoneHeight;
    public float GetSmoothTime() => smoothTime;
    public Vector2 GetOffset() => offset;
}