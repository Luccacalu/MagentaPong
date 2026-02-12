using UnityEngine;

public abstract class BulletBase : MonoBehaviour
{
    [SerializeField] protected int bulletDamage = 1;
    [SerializeField] protected float bulletSpeed = 4f;

    protected bool hasPlayerSignature;
    protected Rigidbody2D rb;
    protected GameSession gameSession;

    protected SpriteRenderer mySpriteRenderer;

    public int Damage => bulletDamage;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        gameSession = Object.FindFirstObjectByType<GameSession>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void ChangeBulletColor()
    {
        if (hasPlayerSignature)
        {
            mySpriteRenderer.color = Color.yellow;
        }
        else
        {
            mySpriteRenderer.color = Color.darkRed;
        }
    }

    protected virtual void Start()
    {
        if (rb.linearVelocity == Vector2.zero)
        {
            rb.linearVelocity = Vector2.right * bulletSpeed;
        }

        ChangeBulletColor();
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
        if (collision.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
        {
            HitResult info = target.TakeDamage(bulletDamage, hasPlayerSignature, collision);

            ApplyHitResult(info);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D thatCollider)
    {
        if (thatCollider.gameObject.TryGetComponent<IDamageable>(out IDamageable target))
        {
            HitResult info = target.TakeDamage(bulletDamage, hasPlayerSignature, null);

            ApplyHitResult(info);
        }
    }

    private void ApplyHitResult(HitResult info)
    {
        if (info.isPlayerTouched.HasValue)
        {
            hasPlayerSignature = info.isPlayerTouched.Value;
            ChangeBulletColor();
        }

        if (info.bounceDirection.HasValue)
        {
            rb.linearVelocity = info.bounceDirection.Value * bulletSpeed;
        }
        else
        {
            rb.linearVelocity = rb.linearVelocity.normalized * bulletSpeed;
        }

        if (info.destroyBullet)
        {
            Destroy(gameObject);
        }
    }
}