using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 10f;

    private Vector2 moveInput;

    void Start()
    {
        
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude < 0.001f)
        {
            return;
        }

        ApplyMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void ApplyMovement()
    {
        Vector2 nextPosition = rb.position + new Vector2(moveInput.x, moveInput.y) * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);
    }
}
