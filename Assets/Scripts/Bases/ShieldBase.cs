using UnityEngine;
using TMPro;

public abstract class ShieldBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected int shieldLife = 1;
    [SerializeField] protected float shieldValue = 0.02f;

    protected TextMeshPro shieldLifeText;
    protected GameSession gameSession;
    protected TeamMember myTeam;

    protected SpriteRenderer _mySpriteRenderer;

    protected virtual void Awake()
    {
        gameSession = Object.FindFirstObjectByType<GameSession>();
        shieldLifeText = GetComponentInChildren<TextMeshPro>();
        myTeam = GetComponent<TeamMember>();
    }

    protected virtual void Start()
    {
        UpdateUI();
    }

    public virtual HitResult TakeDamage(int damage, bool hasPlayerSignature, Collision2D collision = null)
    {
        HitResult result = new HitResult();

        result.destroyBullet = false;
        result.isPlayerTouched = null;

        if (!hasPlayerSignature && myTeam.Team == Team.Enemy)
        {
            return result;
        }

        shieldLife -= damage;
        UpdateUI();

        if (shieldLife <= 0)
        {
            OnShieldDestroyed();
        }

        return result;
    }

    protected virtual void UpdateUI()
    {
        if (shieldLifeText != null)
        {
            shieldLifeText.text = shieldLife.ToString();
        }
    }

    protected virtual void OnShieldDestroyed()
    {
        gameSession.AddMoney(shieldValue);
        Destroy(gameObject);
    }
}