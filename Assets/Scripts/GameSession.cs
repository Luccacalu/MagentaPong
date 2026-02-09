using System;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] public int playerLives = 3;

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

    private void TakeLife()
    {
        playerLives--;
    }
}
