using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    private InputActionReference shootAction;

    public event Action OnShot;

    [SerializeField] private bool isArmed = false;

    private void Awake()
    {
        shootAction = playerData.GetShootAction();
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

    private void OnShootPerformed(InputAction.CallbackContext context)
    {
        if (!isArmed) return;
        
        OnShot?.Invoke();
    }
}
