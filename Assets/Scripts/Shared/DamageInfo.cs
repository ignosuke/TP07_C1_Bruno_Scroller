using UnityEngine;

// Recibe los datos de OnDamaged y cada sistema decide si el golpe le incumbe
public readonly struct DamageInfo
{
    public readonly int Amount;
    public readonly Vector2 Direction;  // Desde la fuente hacia quien recibe
    public readonly int SourceLayer;

    public DamageInfo(int amount, Vector2 direction, int sourceLayer)
    {
        Amount = amount;
        Direction = direction;
        SourceLayer = sourceLayer;
    }

    public bool IsFrom(LayerMask mask) => mask.Contains(SourceLayer);
}