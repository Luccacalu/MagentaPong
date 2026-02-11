using UnityEngine;

public class PlayerHitArea : MonoBehaviour, IDamageable
{
    private GameSession gameSession;

    private void Start()
    {
        gameSession = Object.FindFirstObjectByType<GameSession>();
    }

    public bool TakeDamage(int amount)
    {
        gameSession.ProcessPlayerDamage(amount);

        return true;
    }
}
