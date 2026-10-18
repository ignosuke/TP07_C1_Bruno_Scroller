using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

// Crea paredes invisibles y la zona de vacio a partir del tilemap. Va en el tilemap de terreno, asi hereda su layer (Ground)
public class LevelBounds: MonoBehaviour
{
    private const float Thickness = 3f;
    private const float ExtraHeight = 50f;    // Alto de sobra para que no se escape saltando
    private const float VoidDrop = 6f;        // Cuanto por debajo del nivel empieza el vacio
    private LayerMask lethalLayer;

    private Tilemap boundsTilemap;
    [SerializeField] private HazardSo voidHazardData;

    private void Awake()
    {
        lethalLayer = voidHazardData.GetHazardLayer();

        if (boundsTilemap == null)
            boundsTilemap = GetComponent<Tilemap>();

        Bounds bounds = TilemapBounds.GetWorldBounds(boundsTilemap);

        CreateWalls(bounds);
        CreateVoid(bounds);
    }

    private void CreateWalls(Bounds bounds)
    {
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
        GameObject wall = CreateChild(wallName, center, gameObject.layer);

        wall.AddComponent<BoxCollider2D>().size = size;
    }

    private void CreateVoid(Bounds bounds)
    {
        GameObject zone = CreateChild("VoidHazard",
            new Vector2(bounds.center.x, bounds.min.y - VoidDrop), LayerMaskExtensions.GetLayerIndex(lethalLayer));

        BoxCollider2D collider = zone.AddComponent<BoxCollider2D>();

        collider.isTrigger = true;
        collider.size = new Vector2(bounds.size.x + Thickness * 2f, Thickness);

        zone.AddComponent<Hazard>().SetData(voidHazardData);
    }

    private GameObject CreateChild(string childName, Vector2 position, int layer)
    {
        GameObject child = new GameObject(childName);

        child.transform.SetParent(transform);
        child.transform.position = position;
        child.layer = layer;

        return child;
    }
}