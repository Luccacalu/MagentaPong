using UnityEngine;

public class PlayerPaddle : MonoBehaviour, IDamageable
{
    public HitResult TakeDamage(int amount, bool hasPlayerSignature, Collision2D collision = null)
    {
        HitResult result = new HitResult();
        result.destroyBullet = false;
        result.isPlayerTouched = true;

        if (collision != null)
        {
            result.bounceDirection = CalculateBounce(collision);
        }

        return result;
    }

    private Vector2 CalculateBounce(Collision2D collision)
    {
        Vector2 paddleCenter = transform.position;
        Vector2 hitPoint = collision.contacts[0].point;
        float hitOffset = hitPoint.y - paddleCenter.y;
        float paddleHeight = GetComponent<Collider2D>().bounds.size.y;
        float angle = hitOffset / (paddleHeight / 2);

        return new Vector2(1f, angle).normalized;
    }
}
