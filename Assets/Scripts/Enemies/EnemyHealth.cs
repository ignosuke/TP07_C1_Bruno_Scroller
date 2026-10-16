using UnityEngine;

public class EnemyHealth : Health
{
    [SerializeField] private EnemySo enemyData;
    [SerializeField] private ParticleSystem deathParticlesPrefab;

    protected override int MaxHealth => enemyData.GetMaxHealth();
    protected override LayerMask DamageLayers => enemyData.GetDamageLayers();

    protected override void Die()
    {
        if (deathParticlesPrefab != null)
            Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}