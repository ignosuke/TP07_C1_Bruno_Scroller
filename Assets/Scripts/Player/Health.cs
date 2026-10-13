using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    private int hitpoints;
    private int currentHitpoints;

    public event Action OnDied;
    public event Action OnRevived;

    private void Awake()
    {
        hitpoints = playerData.GetHitpoints();
        currentHitpoints = hitpoints;

    }

    private void FixedUpdate()
    {
        if (currentHitpoints <= 0)
        {
            OnDied?.Invoke();
        }
    }

    public void Revive()
    {
        currentHitpoints = hitpoints;
        OnRevived?.Invoke();
    }
}
