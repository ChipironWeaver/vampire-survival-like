using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HighScore
{
    public string Name;
    public int Score;
    public float Time;
    public void Print()
    {
        Debug.Log(Name +  "Score: " + Score +  " Time: " + Time);
    }
}
[Serializable]
public class HighScores
{
    public Dictionary<string, HighScore> Highscores = new Dictionary<string, HighScore>();

    public void AddHighScore(List<HighScore> highScores)
    {
        foreach (HighScore highScore in highScores)
        {
            Highscores.Add(highScore.Name, highScore);
        }
    }
    public void Print()
    {
        foreach (HighScore highScore in Highscores.Values)
        {
            highScore.Print();
        }
    }
}