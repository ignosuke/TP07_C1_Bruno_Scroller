using UnityEngine;

public class PickupAudio : MonoBehaviour
{
    [SerializeField] private Pickup pickup;
    [SerializeField] private AudioClip pickedClip;

    private void OnEnable()
    {
        pickup.OnPicked += PlayPicked;
    }

    private void OnDisable()
    {
        pickup.OnPicked -= PlayPicked;
    }

    private void PlayPicked() => SfxPlayer.Play(pickedClip);
}