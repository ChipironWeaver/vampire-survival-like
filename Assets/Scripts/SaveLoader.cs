using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoader : MonoBehaviour
{
    public TextAsset jsonFile;
    
    private Dictionary<string, HighScore> Highscores;


    public static SaveLoader Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public HighScores LoadHighScores()
    {
        
        
        HighScores loadedHighscores = JsonUtility.FromJson<HighScores>(jsonFile.text);
        

        //load the Dict from a JSON file
        
        return loadedHighscores;
    }

    public void SaveHighScores(HighScore highScore, HighScores loadedHighscores = null)
    {
        if (loadedHighscores == null) loadedHighscores = LoadHighScores();
        
        if (loadedHighscores.highscores.ContainsKey(highScore.name))
        {
            loadedHighscores.highscores.Remove(highScore.name);
        }
        
        loadedHighscores.highscores.Add(highScore.name, highScore);
        
        //save to json fill
    }

    private void CheckJsonFile()
    {
        
    }

    public int CheckHighScores(HighScore highScore, bool replaceToNewHighScore = true)
    {
        int result = 0; //0 = none, 1 = new high, 2 =  new best time, 3 = both


        return result;
    }
    

    public class HighScore
    {
        public string name;
        public int score;
        public float time;
    }
    
    public class HighScores
    {
        public Dictionary<string, HighScore> highscores;
    }
}
