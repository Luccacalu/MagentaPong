using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 2f;

    private Vector2 moveInput;
    private readonly List<RaycastHit2D> castFilterResults = new List<RaycastHit2D>();
    [SerializeField] private ContactFilter2D contactFilter;

    void Start()
    {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    void Update()
    {
    }

    private void FixedUpdate()
    {
        if (moveInput.sqrMagnitude < 0.001f) return;

        ApplyMovement();
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    private void ApplyMovement()
    {
        Vector2 deltaMovement = moveInput * moveSpeed * Time.fixedDeltaTime;

        if (deltaMovement.x != 0)
        {
            float xDist = Mathf.Abs(deltaMovement.x);
            Vector2 xDir = new Vector2(Mathf.Sign(deltaMovement.x), 0);

            if (CanMove(xDir, xDist, out float hitDist))
            {
                rb.position += xDir * (hitDist - 0.01f);
            }
        }

        if (deltaMovement.y != 0)
        {
            float yDist = Mathf.Abs(deltaMovement.y);
            Vector2 yDir = new Vector2(0, Mathf.Sign(deltaMovement.y));

            if (CanMove(yDir, yDist, out float hitDist))
            {
                rb.position += yDir * (hitDist - 0.01f);
            }
        }
    }

    private bool CanMove(Vector2 direction, float distance, out float validDistance)
    {
        int count = rb.Cast(direction, contactFilter, castFilterResults, distance);

        if (count == 0)
        {
            validDistance = distance;
            return true;
        }

        validDistance = castFilterResults[0].distance;

        return validDistance > 0.01f;
    }
}
