using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private CameraSo cameraData;
    [SerializeField] private Transform target;
    [SerializeField] private Tilemap boundsTilemap; // El tilemap del terreno define los limites

    private float halfWidth;
    private float halfHeight;
    private float smoothTime;
    private Vector2 offset;

    private Camera cam;
    private Bounds levelBounds;
    private bool hasBounds;

    private Vector3 smoothVelocity;

    private void Awake()
    {
        cam = GetComponent<Camera>();

        halfWidth = cameraData.GetDeadZoneWidth() * 0.5f;
        halfHeight = cameraData.GetDeadZoneHeight() * 0.5f;
        smoothTime = cameraData.GetSmoothTime();
        offset = cameraData.GetOffset();

        ResolveTarget();
        CacheLevelBounds();
    }

    private void ResolveTarget()
    {
        if (target != null) return;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null) target = player.transform;
    }

    private void CacheLevelBounds()
    {
        hasBounds = boundsTilemap != null;

        if (!hasBounds) return;

        levelBounds = ExpandWithPadding(TilemapBounds.GetWorldBounds(boundsTilemap));
    }

    // Se expande el limite del nivel con un padding para que haya espacio donde ver al jugador en las plataformas mas altas
    private Bounds ExpandWithPadding(Bounds raw)
    {
        float sides = cameraData.GetPaddingSides();

        Vector3 min = raw.min - new Vector3(sides, cameraData.GetPaddingBottom(), 0f);
        Vector3 max = raw.max + new Vector3(sides, cameraData.GetPaddingTop(), 0f);

        return new Bounds((min + max) * 0.5f, max - min);
    }

    // En LateUpdate para asegurar que el jugador ya termino de moverse este frame
    private void LateUpdate()
    {
        if (target == null) return;

        Vector3 desired = ClampToBounds(ResolveDesiredPosition());

        transform.position = Vector3.SmoothDamp(
            transform.position, desired, ref smoothVelocity, smoothTime);
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

    private Vector3 ClampToBounds(Vector3 position)
    {
        if (!hasBounds) return position;

        // Recalculado cada frame por si cambia la resolucion de la ventana
        float viewHalfHeight = cam.orthographicSize;
        float viewHalfWidth = viewHalfHeight * cam.aspect;

        float x = ClampAxis(position.x,
            levelBounds.min.x + viewHalfWidth, levelBounds.max.x - viewHalfWidth, levelBounds.center.x);

        float y = ClampAxis(position.y,
            levelBounds.min.y + viewHalfHeight, levelBounds.max.y - viewHalfHeight, levelBounds.center.y);

        return new Vector3(x, y, position.z);
    }

    // Si el nivel es mas chico que la pantalla en ese eje, lo centramos en vez de clampear
    private float ClampAxis(float value, float min, float max, float center)
    {
        if (min > max) return center;

        return Mathf.Clamp(value, min, max);
    }

    private void OnDrawGizmosSelected()
    {
        if (cameraData != null)
        {
            Vector2 center = (Vector2)transform.position - cameraData.GetOffset();
            Vector2 size = new Vector2(cameraData.GetDeadZoneWidth(), cameraData.GetDeadZoneHeight());

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(center, size);
        }

        if (boundsTilemap == null || cameraData == null) return;

        Bounds padded = ExpandWithPadding(TilemapBounds.GetWorldBounds(boundsTilemap));

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(padded.center, padded.size);
    }
}