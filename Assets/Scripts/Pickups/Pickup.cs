using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Pickup : MonoBehaviour
{
    // Base de todos los pickups: detecta al jugador, aplica el efecto y se destruye
    // Si el efecto no sirve (vida llena, escudo ya puesto) no se consume
    [SerializeField] private ParticleSystem pickupParticles;

    public event Action OnPicked;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!TryApply(other.gameObject)) return;

        OnPicked?.Invoke();

        if (pickupParticles != null)
            Instantiate(pickupParticles, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    // Devuelve true si el efecto se aplico
    protected abstract bool TryApply(GameObject player);
}