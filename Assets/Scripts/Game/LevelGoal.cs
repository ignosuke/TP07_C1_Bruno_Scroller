using System;
using UnityEngine;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private UIGameManager uiManager;
    [SerializeField] private GameObject goalPickup;

    private int remainingEnemies;

    public bool IsGoalAvailable { get; private set; }

    public event Action OnAllEnemiesDefeated;

    private void Start()
    {
        EnemyHealth[] enemies = FindObjectsByType<EnemyHealth>(FindObjectsSortMode.None);

        remainingEnemies = enemies.Length;

        foreach (EnemyHealth enemy in enemies)
            enemy.OnDied += HandleEnemyDied;

        if (goalPickup != null) goalPickup.SetActive(false);

        if (remainingEnemies == 0) UnlockGoal();
    }

    private void HandleEnemyDied()
    {
        remainingEnemies--;

        if (remainingEnemies > 0) return;

        UnlockGoal();
    }

    private void UnlockGoal()
    {
        IsGoalAvailable = true;
        OnAllEnemiesDefeated?.Invoke();

        // Sin objetivo asignado todavia, matar a todos alcanza para ganar
        if (goalPickup == null)
        {
            CompleteLevel();
            return;
        }

        goalPickup.SetActive(true);
    }

    // La va a llamar el pickup de las alitas cuando el jugador lo agarre
    public void CompleteLevel()
    {
        uiManager.OpenVictory();
    }
}