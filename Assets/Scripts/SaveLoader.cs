using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class SaveLoader : MonoBehaviour
{
    private string destination;
    public static SaveLoader Instance { get; private set; }

    private void Awake()
    {
        Singleton();
        destination = Application.persistentDataPath + "/player_data.json";
    }
    public string GetJsonFromFile()
    {
        string line;
        string json = "";
        try
        {
            FileStream fileStream = File.OpenRead(destination);
            StreamReader streamReader = new(fileStream);

            while ((line = streamReader.ReadLine()) != null)
            {
                json += line;
            }
            fileStream.Close();
        }
        catch (Exception)
        {
            json = "";
        }
        return json;
    }
    
    public void SaveJsonToFile(string json)
    {
        FileStream fileStream = File.OpenWrite(destination);
        StreamWriter streamWriter = new(fileStream);
        //Debug.Log(destination);
        streamWriter.WriteLine(json);
        streamWriter.Close();
        fileStream.Close();
    }
    
    public int CheckHighScores(HighScore highScore, bool replaceToNewHighScore = true)
    {
        int result = 0; //0 = none, 1 = new high, 2 =  new best time, 3 = both
        HighScores loadHighScores = JsonUtility.FromJson<HighScores>(GetJsonFromFile());
        HighScore oldHighScore = loadHighScores.GetHighScore(highScore.name);
        if (highScore.score > oldHighScore.score)
        {
            result ++;
        }
        if (highScore.time > oldHighScore.time)
        {
            result += 2;
        }
        if (replaceToNewHighScore)
        {
            HighScore newHighScore = new  HighScore();
            newHighScore.name = highScore.name;
            newHighScore.score = (int)MathF.Max(highScore.score , oldHighScore.score);
            newHighScore.time = MathF.Max(highScore.time , oldHighScore.time);
            loadHighScores.SetHighScore(newHighScore);
            loadHighScores.Print();
            SaveJsonToFile(JsonUtility.ToJson(loadHighScores));
        }
        return result;
    }
    
    private void Singleton()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
}
