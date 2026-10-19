using UnityEngine;
using UnityEngine.Events;
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

    [Header("Audio")]
    [SerializeField] private AudioClip clickClip;

    private void Awake()
    {
        mainMenuPanel.Open();
        settingsPanel.Close();
        creditsPanel.Close();

        Bind(playButton, Play);
        Bind(settingsButton, () => ShowOnly(settingsPanel));
        Bind(creditsButton, () => ShowOnly(creditsPanel));
        Bind(exitButton, ExitGame);

        Bind(settingsBackButton, () => ShowOnly(mainMenuPanel));
        Bind(creditsBackButton, () => ShowOnly(mainMenuPanel));
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

    // Suscribe la accion y le antepone el sonido de click
    private void Bind(Button button, UnityAction action)
    {
        button.onClick.AddListener(() =>
        {
            SfxPlayer.Play(clickClip);
            action();
        });
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