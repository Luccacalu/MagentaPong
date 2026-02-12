using UnityEngine;

public class EnemyHitArea : MonoBehaviour, IDamageable
{
    private GameSession gameSession;
    [SerializeField] private float areaValueMultiplier = 1f;
    [SerializeField] private float maxValuePerBullet = 5f;

    private void Start()
    {
        gameSession = Object.FindFirstObjectByType<GameSession>();
    }

    public HitResult TakeDamage(int amount, bool hasPlayerSignature, Collision2D collision = null)
    {
        float calculatedValue = areaValueMultiplier * amount;
        float finalValue = Mathf.Min(calculatedValue, maxValuePerBullet);

        if (gameSession != null)
        {
            gameSession.AddMoney(finalValue);
        }

        HitResult result = new HitResult();

        result.destroyBullet = true;
        result.isPlayerTouched = null;

        return result;
    }
}
