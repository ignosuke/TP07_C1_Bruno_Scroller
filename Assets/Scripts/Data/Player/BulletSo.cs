using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "Data/Player/Bullet Data")]
public class BulletSo : ScriptableObject
{
    [Header("Bullet Properties")]
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float lifetime = 3f;
    [SerializeField] private int damage = 1;
    public float GetSpeed() => speed;
    public float GetLifetime() => lifetime;
    public int GetDamage() => damage;

    [Header("Muzzle Flash Properties")]
    [SerializeField] private float muzzleFlashLifetime = .1f;
    public float GetMuzzleFlashLifetime() => muzzleFlashLifetime;

    [Header("Prefabs")]
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private MuzzleFlash muzzleFlashPrefab;
    public Bullet GetBulletPrefab() => bulletPrefab;
    public MuzzleFlash GetMuzzleFlashPrefab() => muzzleFlashPrefab;

    [Header("Collision")]
    [SerializeField] private LayerMask destroyOnHitLayers;
    public LayerMask GetDestroyOnHitLayers() => destroyOnHitLayers;
}
