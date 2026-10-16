using UnityEngine;

public class EnemyDamage : MonoBehaviour, IDamageSource
{
    [SerializeField] private EnemySo enemyData;

    public int Damage => enemyData.GetContactDamage();
}