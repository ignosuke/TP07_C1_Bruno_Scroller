using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    [SerializeField] private BulletSo bulletData;

    private void Start()
    {
        Destroy(gameObject, bulletData.GetMuzzleFlashLifetime());
    }
}
