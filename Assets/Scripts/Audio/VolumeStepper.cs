using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

// Barra de volumen que sube y baja de a pasos fijos con dos botones
public class VolumeStepper : MonoBehaviour
{
    private const float Step = 0.1f;
    private const float Epsilon = 0.001f; // Margen para comparar floats en los extremos

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string parameter; // Nombre del parametro expuesto en el mixer

    [Header("UI")]
    [SerializeField] private Button decreaseButton;
    [SerializeField] private Button increaseButton;
    [SerializeField] private RectTransform fillRect; // Pivot en X = 0 para que crezca desde la izquierda

    private float volume;

    private void Awake()
    {
        decreaseButton.onClick.AddListener(Decrease);
        increaseButton.onClick.AddListener(Increase);
    }

    private void OnDestroy()
    {
        decreaseButton.onClick.RemoveAllListeners();
        increaseButton.onClick.RemoveAllListeners();
    }

    private void OnEnable()
    {
        // Al abrir el panel leemos lo guardado, sin volver a guardarlo
        SetVolume(AudioVolume.GetSaved(parameter), false);
    }

    private void OnDisable()
    {
        PlayerPrefs.Save();
    }

    private void Decrease() => SetVolume(volume - Step, true);

    private void Increase() => SetVolume(volume + Step, true);

    private void SetVolume(float newVolume, bool save)
    {
        volume = Mathf.Clamp01(Snap(newVolume));

        AudioVolume.Apply(mixer, parameter, volume);

        if (save) AudioVolume.Save(parameter, volume);

        Refresh();
    }

    // Alinea cualquier valor guardado a la grilla de pasos
    private float Snap(float raw)
    {
        return Mathf.Round(raw / Step) * Step;
    }

    private void Refresh()
    {
        fillRect.localScale = new Vector3(volume, 1f, 1f);

        decreaseButton.interactable = volume > Epsilon;
        increaseButton.interactable = volume < 1f - Epsilon;
    }
}