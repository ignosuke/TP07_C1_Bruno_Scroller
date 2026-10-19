using UnityEngine;

public class EnemyDrop : MonoBehaviour
{
    // Suelta un pickup al morir. El prefab se lo asigna el PickupDropAssigner al empezar el nivel
    [SerializeField] private EnemyHealth health;

    private GameObject dropPrefab;

    public bool HasDrop => dropPrefab != null;

    public void SetDrop(GameObject prefab) => dropPrefab = prefab;

    private void OnEnable()
    {
        health.OnDied += SpawnDrop;
    }

    private void OnDisable()
    {
        health.OnDied -= SpawnDrop;
    }

    // OnDied se dispara antes de que EnemyHealth se destruya, asi el pickup alcanza a nacer
    private void SpawnDrop()
    {
        if (!HasDrop) return;

        Instantiate(dropPrefab, transform.position, Quaternion.identity);
    }
}