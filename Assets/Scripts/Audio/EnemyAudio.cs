using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
    [SerializeField] private EnemyHealth health;

    [Header("Clips")]
    [SerializeField] private AudioClip hitClip;   // La bala le pega
    [SerializeField] private AudioClip diedClip;

    private void OnEnable()
    {
        health.OnDamaged += PlayHit;
        health.OnDied += PlayDied;
    }

    private void OnDisable()
    {
        health.OnDamaged -= PlayHit;
        health.OnDied -= PlayDied;
    }

    private void PlayHit(DamageInfo info) => SfxPlayer.Play(hitClip);
    private void PlayDied() => SfxPlayer.Play(diedClip);
}