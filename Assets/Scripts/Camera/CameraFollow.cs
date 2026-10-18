using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CameraSo cameraData;
    [SerializeField] private Transform target;

    private float halfWidth;
    private float halfHeight;
    private float smoothTime;
    private Vector2 offset;

    private Vector3 smoothVelocity;

    private void Awake()
    {
        halfWidth = cameraData.GetDeadZoneWidth() * 0.5f;
        halfHeight = cameraData.GetDeadZoneHeight() * 0.5f;
        smoothTime = cameraData.GetSmoothTime();
        offset = cameraData.GetOffset();
    }

    // En LateUpdate para asegurar que el jugador ya termino de moverse este frame
    private void LateUpdate()
    {
        if (target == null) return;

        transform.position = Vector3.SmoothDamp(
            transform.position, ResolveDesiredPosition(), ref smoothVelocity, smoothTime);
    }

    // Mientras el jugador este dentro del rectangulo, el destino es la posicion actual: no se mueve
    private Vector3 ResolveDesiredPosition()
    {
        Vector2 center = GetDeadZoneCenter();
        Vector2 delta = (Vector2)target.position - center;

        float x = center.x;
        float y = center.y;

        if (Mathf.Abs(delta.x) > halfWidth)
            x = target.position.x - Mathf.Sign(delta.x) * halfWidth;

        if (Mathf.Abs(delta.y) > halfHeight)
            y = target.position.y - Mathf.Sign(delta.y) * halfHeight;

        return new Vector3(x + offset.x, y + offset.y, transform.position.z);
    }

    private Vector2 GetDeadZoneCenter()
    {
        return (Vector2)transform.position - offset;
    }

    private void OnDrawGizmosSelected()
    {
        if (cameraData == null) return;

        Vector2 center = (Vector2)transform.position - cameraData.GetOffset();
        Vector2 size = new Vector2(cameraData.GetDeadZoneWidth(), cameraData.GetDeadZoneHeight());

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(center, size);
    }
}