using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class HighScore
{
    public string name;
    public int score;
    public float time;
    public void Print()
    {
        Debug.Log(name +  " Score: " + score +  " Time: " + time);
    }
}
[Serializable]
public class HighScores
{
    public List<string> keys = new List<string>();
    public List<float> values = new List<float>();

    public void SetHighScore(HighScore highScore)
    {
        if(keys.Contains(highScore.name + "Score")) 
            values[keys.IndexOf(highScore.name + "Score")] = highScore.score;
        else
        {
            keys.Add(highScore.name + "Score");
            values.Add(highScore.score);
        }
        if(keys.Contains(highScore.name + "Time")) 
            values[keys.IndexOf(highScore.name + "Time")] = highScore.time;
        else
        {
            keys.Add(highScore.name + "Time");
            values.Add(highScore.time);
        }
    }
    public HighScore GetHighScore(string name)
    {
        HighScore high =  new HighScore();
        high.name = name;
        if (keys.Contains(name + "Score")) high.score = (int)values[keys.IndexOf(name + "Score")]; 
        if (keys.Contains(name + "Time")) high.time = values[keys.IndexOf(name + "Time")];
        return high;
    }

    public void RemoveHighScore(string name)
    {
        
    }
    public void Print()
    {
        for(int i = 0; i < keys.Count; i++)
        {
            Debug.Log(keys.ElementAt(i) + "Score: " + values.ElementAt(i));
        }
    }
}