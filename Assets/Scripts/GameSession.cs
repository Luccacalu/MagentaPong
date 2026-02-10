using System;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;

    [SerializeField] TextMeshProUGUI livesText;

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
}
