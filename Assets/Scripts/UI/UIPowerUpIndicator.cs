using TMPro;
using UnityEngine;

public class UIPowerUpIndicator : MonoBehaviour
{
    // Muestra el panel del power up de cadencia con los segundos que le quedan
    [SerializeField] private PlayerShooter shooter;
    [SerializeField] private UIPanel panel;
    [SerializeField] private TMP_Text timeText;

    private void Awake()
    {
        panel.Close();
    }

    private void OnEnable()
    {
        shooter.OnBoostStarted += HandleBoostStarted;
        shooter.OnBoostEnded += HandleBoostEnded;
    }

    private void OnDisable()
    {
        shooter.OnBoostStarted -= HandleBoostStarted;
        shooter.OnBoostEnded -= HandleBoostEnded;
    }

    private void HandleBoostStarted() => panel.Open();

    private void HandleBoostEnded() => panel.Close();

    private void Update()
    {
        if (!shooter.IsBoosted) return;

        // CeilToInt para que muestre 1 hasta que realmente llegue a cero
        int seconds = Mathf.CeilToInt(shooter.BoostTimeRemaining);
        timeText.text = $"{seconds / 60:00}:{seconds % 60:00}";
    }
}