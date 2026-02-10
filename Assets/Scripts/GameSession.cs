using System;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;
    [SerializeField] public float playerMoney = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI moneyText;

    private void Awake()
    {
        int numGameSession = FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;

        if(numGameSession > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        livesText.text = playerLives.ToString();
        moneyText.text = "$" + playerMoney.ToString("F2");
    }

    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        Debug.Log("Perdeu");
    }

    public void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();
    }

    public void SpendMoney(float price)
    {
        playerMoney -= price;
        moneyText.text = "$" + playerMoney.ToString("F2");
    }

    public void AddMoney(float amount)
    {
        playerMoney += amount;
        moneyText.text = "$" + playerMoney.ToString("F2");
    }
}
