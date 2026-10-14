using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    [SerializeField] private BulletSo bulletData;

    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform firePoint;

    private InputActionReference shootAction;

    public event Action OnShot;

    private float fireCooldown;
    private float cooldownTimer;

    [SerializeField] private bool isArmed = false;

    private void Awake()
    {
        shootAction = playerData.GetShootAction();

        fireCooldown = 1f / Mathf.Max(playerData.GetFireRate(), 0.1f); // Evitar division por cero, cooldown infinito
    }

    private void OnEnable()
    {
        shootAction.action.performed += OnShootPerformed;
        shootAction.action.Enable();
    }

    private void OnDisable()
    {
        shootAction.action.performed -= OnShootPerformed;
        shootAction.action.Disable();
    }

    private void Update()
    {
        if (cooldownTimer >= 0) cooldownTimer -= Time.deltaTime;
    }

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (!isArmed || cooldownTimer > 0) return;

        cooldownTimer = fireCooldown;

        Instantiate(bulletData.GetMuzzleFlashPrefab(), muzzlePoint.position, muzzlePoint.rotation, muzzlePoint);
        Instantiate(bulletData.GetBulletPrefab(), firePoint.position, firePoint.rotation);

        OnShot?.Invoke();
    }
}
