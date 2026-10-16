using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    private InputActionReference moveAction;
    private Rigidbody2D rb;

    private float moveSpeed = 10.0f;
    private Vector2 moveDir;

    private void Awake()
    {
        moveAction = playerData.GetMoveAction();
        moveSpeed = playerData.GetMoveSpeed();

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        moveDir = moveAction.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveDir.x * moveSpeed, rb.linearVelocity.y);
    }
}
