using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private BulletSo bulletData;

    private Rigidbody2D rb;

    private float speed;
    private float lifetime;
    private int damage;

    private LayerMask destroyOnHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        speed = bulletData.GetSpeed();
        lifetime = bulletData.GetLifetime();
        damage = bulletData.GetDamage();

        destroyOnHit = bulletData.GetDestroyOnHitLayers();
    }

    private void Start()
    {
        rb.linearVelocity = transform.right * speed;
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!IsInMask(other.gameObject.layer)) return;

        Destroy(gameObject);
    }

    private bool IsInMask(int layer)
    {
        return (destroyOnHit.value & (1 << layer)) != 0;
    }
}
