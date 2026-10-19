using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SfxPlayer : MonoBehaviour
{
    // Un solo reproductor de sfx por escena
    public static SfxPlayer Instance { get; private set; }

    private AudioSource source;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        source = GetComponent<AudioSource>();
    }

    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }

    public static void Play(AudioClip clip)
    {
        if (clip == null || Instance == null) return;

        Instance.source.PlayOneShot(clip);
    }
}