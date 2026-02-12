using UnityEngine;

public struct HitResult
{
    public bool destroyBullet;
    public bool? isPlayerTouched;
    public Vector2? bounceDirection;
}

public interface IDamageable
{
    HitResult TakeDamage(int amount, bool isPlayerTouched, Collision2D collision = null);
}