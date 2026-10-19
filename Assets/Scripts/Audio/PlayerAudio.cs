using UnityEngine;

// Escucha los eventos que el jugador ya emitia y les pone sonido
public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private PlayerJumper jumper;
    [SerializeField] private PlayerShooter shooter;
    [SerializeField] private PlayerHealth health;

    [Header("Clips")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip shootClip;
    [SerializeField] private AudioClip damagedClip;
    [SerializeField] private AudioClip diedClip;


    private void OnEnable()
    {
        jumper.OnJumped += PlayJump;
        shooter.OnShot += PlayShoot;
        health.OnDamaged += PlayDamaged;
        health.OnDied += PlayDied;
    }

    private void OnDisable()
    {
        jumper.OnJumped -= PlayJump;
        shooter.OnShot -= PlayShoot;
        health.OnDamaged -= PlayDamaged;
        health.OnDied -= PlayDied;
    }

    private void PlayJump() => SfxPlayer.Play(jumpClip);
    private void PlayShoot() => SfxPlayer.Play(shootClip);
    private void PlayDamaged(DamageInfo info) => SfxPlayer.Play(damagedClip);
    private void PlayDied() => SfxPlayer.Play(diedClip);
}