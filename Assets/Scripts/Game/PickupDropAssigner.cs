using UnityEngine;

public class PickupDropAssigner : MonoBehaviour
{
    // Reparte un pickup unico por nivel entre enemigos elegidos al azar
    [SerializeField] private GameObject[] dropPrefabs; // Todos los prefabs de pickups dropeables. Curacion, escudo y cadencia

    private void Start()
    {
        EnemyDrop[] carriers = FindObjectsByType<EnemyDrop>(FindObjectsSortMode.None);

        Shuffle(carriers);

        int count = Mathf.Min(dropPrefabs.Length, carriers.Length);

        for (int i = 0; i < count; i++)
            carriers[i].SetDrop(dropPrefabs[i]);

        // Debug por si hay mas pickups que enemigos
        if (dropPrefabs.Length > carriers.Length)
            Debug.LogWarning($"Hay {dropPrefabs.Length} pickups para {carriers.Length} enemigos: algunos no se reparten.");
    }

    // Se mezclan los enemigos y se asignan los pickups a los primeros aleatoriamente
    private void Shuffle(EnemyDrop[] items)
    {
        for (int i = items.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            (items[i], items[j]) = (items[j], items[i]);
        }
    }
}