using UnityEngine;
using UnityEngine.Tilemaps;

// Helper compartido: la camara y las paredes del nivel leen los limites del mismo lugar
public static class TilemapBounds
{
    public static Bounds GetWorldBounds(Tilemap tilemap)
    {
        tilemap.CompressBounds(); // Recorta el area vacia: los limites son lo que realmente pintaste

        Bounds local = tilemap.localBounds;

        return new Bounds(tilemap.transform.TransformPoint(local.center), local.size);
    }
}