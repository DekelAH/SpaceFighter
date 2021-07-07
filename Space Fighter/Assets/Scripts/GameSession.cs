using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    int score = 0; // Game score

    private void Awake()
    {
        SetUpSingleton(); 
    }

    private void SetUpSingleton()


    {
        // Setting the number of sessions in game to variable
        int numGameSessions = FindObjectsOfType<GameSession>().Length;

        // If more than one game session game object ~> destroy it
        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        } 
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore()
    {
        return score; // Getting the score
    }

    public void AddToScore(int scoreValue)
    {
        score += scoreValue; // Adding score to current score
    }

    public void ResetGame()
    {
        Destroy(gameObject); // Destroying the game object for reseting score
    }

}
