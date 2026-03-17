using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoader : MonoBehaviour
{
    
    
    [SerializeField] private List<HighScore> testHighScores;
    [SerializeField] private HighScore testHighScore;
    public Dictionary<string, float> testdict = new Dictionary<string, float>();
    private string destination;
    public static SaveLoader Instance { get; private set; }

    private void Awake()
    {
        Singleton();
        destination = Application.persistentDataPath + "/player_data.json";
    }

    private void Start()
    {
        
        HighScores test = new HighScores();
        test.AddHighScore(testHighScores);
        test.Print();
        print(JsonUtility.ToJson(testHighScores[1], true));
        print(JsonUtility.ToJson(testHighScores, true));
        
        //print(GetJsonFromFile());
        /*HighScores test2 = LoadHighScores();
        test2.Print();*/
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
        Debug.Log(destination);
        streamWriter.WriteLine(json);
        streamWriter.Close();
        fileStream.Close();
    }
    /*public void SaveHighScores(HighScore highScore, HighScores loadedHighscores = null)
    {
        loadedHighscores ??= LoadHighScores();
        
        loadedHighscores.Highscores.Remove(highScore.Name);
        
        loadedHighscores.Highscores.Add(highScore.Name, highScore);
        
        JsonUtility.FromJsonOverwrite(jsonFile.text, loadedHighscores);
    }*/
    

    public int CheckHighScores(HighScore highScore, bool replaceToNewHighScore = true)
    {
        int result = 0; //0 = none, 1 = new high, 2 =  new best time, 3 = both


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
