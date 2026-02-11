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

    public bool TakeDamage(int amount)
    {
        float calculatedValue = areaValueMultiplier * amount;
        float finalValue = Mathf.Min(calculatedValue, maxValuePerBullet);

        if (gameSession != null)
        {
            gameSession.AddMoney(finalValue);
        }

        return true;
    }
}
