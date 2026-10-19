using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private UIPanel mainMenuPanel;
    [SerializeField] private UIPanel settingsPanel;
    [SerializeField] private UIPanel creditsPanel;

    [Header("Main Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button exitButton;

    [Header("Settings Menu Buttons")]
    [SerializeField] private Button settingsBackButton;

    [Header("Credits Menu Buttons")]
    [SerializeField] private Button creditsBackButton;

    private void Awake()
    {
        // Main Menu abierto al inicio, el resto oculto
        mainMenuPanel.Open();
        settingsPanel.Close();
        creditsPanel.Close();

        // Listeners
        playButton.onClick.AddListener(Play);
        settingsButton.onClick.AddListener(() => ShowOnly(settingsPanel));
        creditsButton.onClick.AddListener(() => ShowOnly(creditsPanel));
        exitButton.onClick.AddListener(ExitGame);

        settingsBackButton.onClick.AddListener(() => ShowOnly(mainMenuPanel));
        creditsBackButton.onClick.AddListener(() => ShowOnly(mainMenuPanel));
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        creditsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();

        settingsBackButton.onClick.RemoveAllListeners();
        creditsBackButton.onClick.RemoveAllListeners();
    }

    // Botones
    private void Play()
    {
        SceneManager.LoadScene("GameScene");
    }

    // Los paneles se muestran encima y se ocultan al salir
    private void ShowOnly(UIPanel panel)
    {
        mainMenuPanel.Close();
        settingsPanel.Close();
        creditsPanel.Close();

        panel.Open();
    }

    private void ExitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}