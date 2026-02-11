using UnityEngine;

public class SimpleBullet : BulletBase
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}