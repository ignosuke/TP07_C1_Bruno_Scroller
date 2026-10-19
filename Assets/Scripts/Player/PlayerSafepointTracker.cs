using UnityEngine;

// Va guardando la ultima posicion firme del jugador para poder devolverlo ahi
public class SafePointTracker : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    [SerializeField] private PlayerGroundChecker groundChecker;

    private float interval;
    private float counter;

    public Vector3 LastSafePoint { get; private set; }

    private void Awake()
    {
        interval = playerData.GetSafePointInterval();
        LastSafePoint = transform.position;
    }

    private void Update()
    {
        if (!groundChecker.GetIsFullyGrounded()) return;

        counter -= Time.deltaTime;

        if (counter > 0f) return;

        counter = interval;
        LastSafePoint = transform.position;
    }
}