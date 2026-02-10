using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] public float bulletSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.IsTouchingLayers(LayerMask.GetMask("Foreground")))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, -rb.linearVelocity.y);
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 paddleCenter = collision.transform.position;
            Vector2 hitPoint = collision.contacts[0].point;

            float hitOffset = hitPoint.y - paddleCenter.y;
            float paddleHeight = collision.collider.bounds.size.y;

            float angle = hitOffset / (paddleHeight / 2);

            Vector2 newDirection = new Vector2(-1, angle).normalized;
            rb.linearVelocity = newDirection * bulletSpeed;

            Vector2 newPosition = new Vector2(transform.position.x, hitPoint.y);
            transform.position = newPosition;
        }
        else if (collision.gameObject.CompareTag("PlayerHitArea"))
        {
            Object.FindFirstObjectByType<GameSession>().TakeLife();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EnemyHitArea"))
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
    }
}
