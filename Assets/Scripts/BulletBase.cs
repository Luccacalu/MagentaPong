using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected int bulletDamage = 1;
    [SerializeField] protected float bulletSpeed = 4f;

    protected Rigidbody2D rb;
    protected GameSession gameSession;
    public int Damage => bulletDamage;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameSession = Object.FindFirstObjectByType<GameSession>();
    }

    protected virtual void Start()
    {
        if (rb.linearVelocity == Vector2.zero)
        {
            rb.linearVelocity = Vector2.right * bulletSpeed;
        }
    }

    protected virtual void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > 0 && Mathf.Abs(rb.linearVelocity.magnitude - bulletSpeed) > 0.1f)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * bulletSpeed;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BounceOffPaddle(collision);
        }

        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
        {
            bool shouldSelfDestroy = target.TakeDamage(bulletDamage);

            if (shouldSelfDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                rb.linearVelocity = rb.linearVelocity.normalized * bulletSpeed;
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IDamageable>(out IDamageable target))
        {
            bool shouldSelfDestroy = target.TakeDamage(bulletDamage);

            if (shouldSelfDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                rb.linearVelocity = rb.linearVelocity.normalized * bulletSpeed;
            }
        }
    }

    protected void BounceOffPaddle(Collision2D collision)
    {
        Vector2 paddleCenter = collision.transform.position;
        Vector2 hitPoint = collision.contacts[0].point;
        float hitOffset = hitPoint.y - paddleCenter.y;
        float paddleHeight = collision.collider.bounds.size.y;
        float angle = hitOffset / (paddleHeight / 2);

        Vector2 newDirection = new Vector2(1f, angle).normalized;
        rb.linearVelocity = newDirection * bulletSpeed;
    }
}