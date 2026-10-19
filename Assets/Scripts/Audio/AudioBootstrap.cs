using UnityEngine;
using UnityEngine.Audio;

public class AudioBootstrap : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private string[] parameters; // MasterVolume, MusicVolume, SfxVolume

    private void Start()
    {
        foreach (string parameter in parameters)
            AudioVolume.Apply(mixer, parameter, AudioVolume.GetSaved(parameter));
    }
}