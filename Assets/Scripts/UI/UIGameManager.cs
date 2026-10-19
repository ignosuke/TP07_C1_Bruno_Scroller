using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    private const float GameOverDelay = 1.2f;
    private const int MainMenuSceneIndex = 0;

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private InputActionReference pauseAction;

    [Header("Panels")]
    [SerializeField] private UIPanel pausePanel;
    [SerializeField] private UIPanel gameOverPanel;
    [SerializeField] private UIPanel victoryPanel;

    [Header("Buttons")]
    [SerializeField] private Button victoryBackToMenu;
    [SerializeField] private Button gameOverBackToMenu;
    [SerializeField] private Button pauseBackToMenu;
    [SerializeField] private Button gameOverRetry;

    private bool isRunEnded;

    private void Awake()
    {
        pausePanel.Close();
        gameOverPanel.Close();
        victoryPanel.Close();

        victoryBackToMenu.onClick.AddListener(GoToMenu);
        gameOverBackToMenu.onClick.AddListener(GoToMenu);
        pauseBackToMenu.onClick.AddListener(GoToMenu);
        gameOverRetry.onClick.AddListener(Retry);
    }

    private void OnEnable()
    {
        playerHealth.OnDied += HandleDeath;

        pauseAction.action.performed += OnPausePerformed;
        pauseAction.action.Enable();
    }

    private void OnDisable()
    {
        playerHealth.OnDied -= HandleDeath;

        pauseAction.action.performed -= OnPausePerformed;
        pauseAction.action.Disable();

        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        victoryBackToMenu.onClick.RemoveAllListeners();
        gameOverBackToMenu.onClick.RemoveAllListeners();
        pauseBackToMenu.onClick.RemoveAllListeners();
        gameOverRetry.onClick.RemoveAllListeners();
    }

    private void HandleDeath()
    {
        isRunEnded = true;
        StartCoroutine(OpenAfterDelay(gameOverPanel));
    }

    private IEnumerator OpenAfterDelay(UIPanel panel)
    {
        yield return new WaitForSeconds(GameOverDelay);

        panel.Open();
        Time.timeScale = 0f;
    }

    public void OpenVictory()
    {
        if (isRunEnded) return;

        isRunEnded = true;
        victoryPanel.Open();
        Time.timeScale = 0f;
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (isRunEnded) return;

        SetPaused(!pausePanel.IsOpen());
    }

    private void SetPaused(bool value)
    {
        if (value)
            pausePanel.Open();
        else
            pausePanel.Close();

        Time.timeScale = value ? 0f : 1f;
    }

    public void Resume() => SetPaused(false);

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuSceneIndex);
    }
}