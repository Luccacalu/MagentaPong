using UnityEngine;

public class PlayerHitArea : MonoBehaviour, IDamageable
{
    private GameSession gameSession;

    private void Start()
    {
        gameSession = Object.FindFirstObjectByType<GameSession>();
    }

    public HitResult TakeDamage(int amount, bool hasPlayerSignature, Collision2D collision = null)
    {
        gameSession.ProcessPlayerDamage(amount);

        HitResult result = new HitResult();

        result.isPlayerTouched = null;
        result.destroyBullet = true;

        return result;
    }
}
