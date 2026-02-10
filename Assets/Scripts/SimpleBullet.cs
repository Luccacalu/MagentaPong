using UnityEngine;

public class SimpleBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameSession gameSession;
    [SerializeField] public float bulletSpeed = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameSession = Object.FindFirstObjectByType<GameSession>();
        if (rb.linearVelocity == Vector2.zero)
        {
            rb.linearVelocity = Vector2.right * bulletSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 paddleCenter = collision.transform.position;
            Vector2 hitPoint = collision.contacts[0].point;

            float hitOffset = hitPoint.y - paddleCenter.y;
            float paddleHeight = collision.collider.bounds.size.y;

            float angle = hitOffset / (paddleHeight / 2);

            Vector2 newDirection = new Vector2(1f, angle).normalized;
            rb.linearVelocity = newDirection * bulletSpeed;
        }
        else if (collision.gameObject.CompareTag("PlayerHitArea"))
        {
            gameSession.TakeLife();
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("EnemyHitArea"))
        {
            gameSession.AddMoney(0.05f);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > 0 && Mathf.Abs(rb.linearVelocity.magnitude - bulletSpeed) > 0.1f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * bulletSpeed;
        }
    }

    void Update()
    {
        
    }
}
