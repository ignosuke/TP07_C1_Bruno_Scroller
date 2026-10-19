using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Data/Enemies/Enemy Data")]
public class EnemySo : ScriptableObject
{
    [Header("Stats")]
    [SerializeField, Min(1)] private int maxHealth = 3;
    [SerializeField, Min(1)] private int contactDamage = 1;
    public int GetMaxHealth() => maxHealth;
    public int GetContactDamage() => contactDamage;

    [Header("Layers")]
    [SerializeField] private LayerMask damageLayers;   // Capas que dañan al enemigo
    public LayerMask GetDamageLayers() => damageLayers;
}