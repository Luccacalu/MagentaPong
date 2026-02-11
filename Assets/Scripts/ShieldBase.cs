using UnityEngine;
using TMPro;

public abstract class ShieldBase : MonoBehaviour, IDamageable
{
    [SerializeField] protected int shieldLife = 1;
    [SerializeField] protected float shieldValue = 0.02f;

    protected TextMeshPro shieldLifeText;
    protected GameSession gameSession;

    protected virtual void Awake()
    {
        gameSession = Object.FindFirstObjectByType<GameSession>();
        shieldLifeText = GetComponentInChildren<TextMeshPro>();
    }

    protected virtual void Start()
    {
        UpdateUI();
    }

    public virtual bool TakeDamage(int damage)
    {
        shieldLife -= damage;
        UpdateUI();

        if (shieldLife <= 0)
        {
            OnShieldDestroyed();
        }

        return false;
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