using UnityEngine;
using UnityEngine.Tilemaps;

// Crea paredes invisibles en los extremos del nivel a partir del tilemap. Componente del tilemap de terreno, asi hereda el layer Ground y obtiene el tilemap con GetComponent<>.
public class LevelWalls : MonoBehaviour
{
    private const float Thickness = 1f;
    private const float ExtraHeight = 50f; // Alto de sobra para que no se escape saltando

    private Tilemap boundsTilemap;

    private void Awake()
    {
        if (boundsTilemap == null)
            boundsTilemap = GetComponent<Tilemap>();

        Bounds bounds = TilemapBounds.GetWorldBounds(boundsTilemap);

        float height = bounds.size.y + ExtraHeight;
        float halfThickness = Thickness * 0.5f;

        CreateWall("WallLeft",
            new Vector2(bounds.min.x - halfThickness, bounds.center.y),
            new Vector2(Thickness, height));

        CreateWall("WallRight",
            new Vector2(bounds.max.x + halfThickness, bounds.center.y),
            new Vector2(Thickness, height));
    }

    private void CreateWall(string wallName, Vector2 center, Vector2 size)
    {
        GameObject wall = new GameObject(wallName);

        wall.transform.SetParent(transform);
        wall.transform.position = center;
        wall.layer = gameObject.layer;

        wall.AddComponent<BoxCollider2D>().size = size;
    }
}