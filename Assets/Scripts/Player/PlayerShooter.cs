using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    [SerializeField] private BulletSo bulletData;

    [SerializeField] private Transform muzzlePoint;
    [SerializeField] private Transform firePoint;

    private InputActionReference shootAction;

    public event Action<bool> OnArmedChanged;
    public event Action OnShot;
    public event Action OnBoostStarted;
    public event Action OnBoostEnded;

    private float fireCooldown;
    private float cooldownTimer;
    public float BoostTimeRemaining { get; private set; }
    public bool IsBoosted => BoostTimeRemaining > 0f;
    private Coroutine boostRoutine;

    private bool isArmed = false;

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

    public bool TryArm()
    {
        if (isArmed) return false;

        isArmed = true;
        OnArmedChanged?.Invoke(true);
        return true;
    }

    public void ApplyFireRateBoost(float rate, float duration)
    {
        if (boostRoutine != null) StopCoroutine(boostRoutine);

        boostRoutine = StartCoroutine(FireRateBoost(rate, duration));
    }

    private IEnumerator FireRateBoost(float rate, float duration)
    {
        fireCooldown = 1f / rate;
        BoostTimeRemaining = duration;

        OnBoostStarted?.Invoke();

        while (BoostTimeRemaining > 0f)
        {
            BoostTimeRemaining -= Time.deltaTime;
            yield return null;
        }

        fireCooldown = 1f / playerData.GetFireRate();

        OnBoostEnded?.Invoke();
        boostRoutine = null;
    }
}
