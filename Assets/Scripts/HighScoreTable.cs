using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScoreTable : MonoBehaviour
{
    [SerializeField] GameObject[] highScorePanels;
    private List<HighScoreEntry> highScoreEntriesList;
    [SerializeField] private Text Newname;
    [SerializeField] private GardenManager gardenManager;
    [SerializeField] private bool alreadyadded;

    private void Awake()
    {
        alreadyadded = false;
        highScorePanels = GameObject.FindGameObjectsWithTag("HighScoreEntry");
        gardenManager = FindObjectOfType<GardenManager>();
        UpdateHighScoreTable();
    }

    /// <summary>
    /// Actualiza los paneles del leader board con los datos guardados en memoria
    /// </summary>
    public void UpdateHighScoreTable()
    {
        string jsonString = PlayerPrefs.GetString("LeaderBoardTable");
        if (jsonString == "")
        {
            highScoreEntriesList = new List<HighScoreEntry>()
            {
                new HighScoreEntry{ score =  0, name = "JSV"}
            };
            HighScores highScores = new HighScores { highScoreEntriesList = highScoreEntriesList };
            jsonString = JsonUtility.ToJson(highScores);
            PlayerPrefs.SetString("LeaderBoardTable", jsonString);
            PlayerPrefs.Save();
        }
        else
        {
            HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
            for (int i = 0; i < highScores.highScoreEntriesList.Count; i++)
            {
                for (int j = i + 1; j < highScores.highScoreEntriesList.Count; j++)
                {
                    if (highScores.highScoreEntriesList[j].score > highScores.highScoreEntriesList[i].score)
                    {
                        HighScoreEntry tmp = highScores.highScoreEntriesList[i];
                        highScores.highScoreEntriesList[i] = highScores.highScoreEntriesList[j];
                        highScores.highScoreEntriesList[j] = tmp;
                    }
                }
            }
            for (int i = 0; i < highScorePanels.Length; i++)
            {

                int rank = i + 1;
                string rankString;
                switch (rank)
                {
                    case 1: rankString = "1st"; break;
                    case 2: rankString = "2nd"; break;
                    case 3: rankString = "3rd"; break;
                    default: rankString = rank + "th"; break;
                }
                highScorePanels[i].transform.Find("PosText").GetComponent<Text>().text = rankString;
                if (i < highScores.highScoreEntriesList.Count)
                {
                    highScorePanels[i].transform.Find("ScoreText").GetComponent<Text>().text = highScores.highScoreEntriesList[i].score.ToString();
                    PlayerPrefs.SetInt("HighScore", highScores.highScoreEntriesList[i].score);
                    highScorePanels[i].transform.Find("NameText").GetComponent<Text>().text = highScores.highScoreEntriesList[i].name;
                }
            }
        }
    }

    /// <summary>
    /// Adiciona un puntaje al leader board guardado en memoria de ser necesario
    /// </summary>
    public void AddHighScoreEntry()
    {

        HighScoreEntry highScoreEntry = new HighScoreEntry { score = gardenManager.score, name = Newname.text };
        string jsonString = PlayerPrefs.GetString("LeaderBoardTable");
        HighScores highScores = JsonUtility.FromJson<HighScores>(jsonString);
        int placetoaddscore = 0;
        bool scoreAdded = false;
        for (int i = 0; i < highScores.highScoreEntriesList.Count; i++)
        {
            if (highScoreEntry.score == highScores.highScoreEntriesList[i].score)
            {
                alreadyadded = true;
                break;
            }
            else
                alreadyadded = false;
        }
        if (alreadyadded)
        {
            return;
        }
        for (int i = 0; i < highScores.highScoreEntriesList.Count; i++)
        {
            if (highScoreEntry.score > highScores.highScoreEntriesList[i].score)
            {
                placetoaddscore = i;
                scoreAdded = true;
                break;
            }
        }
        if (scoreAdded)
        {
            highScores.highScoreEntriesList.Insert(placetoaddscore, highScoreEntry);
            alreadyadded = true;
        }
        if (!scoreAdded && highScores.highScoreEntriesList.Count < 5)
        {
            highScores.highScoreEntriesList.Add(highScoreEntry);
            alreadyadded = true;
        }
        if (highScores.highScoreEntriesList.Count > 5)
        {
            highScores.highScoreEntriesList.RemoveRange(5, highScores.highScoreEntriesList.Count - 5);
        }
        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("LeaderBoardTable", json);
        PlayerPrefs.Save();
        UpdateHighScoreTable();
    }
    /// <summary>
    /// Resetea los valores guardados en memoria de leader board
    /// </summary>
    public void ResetLeaderBoard()
    {
        PlayerPrefs.DeleteKey("LeaderBoardTable");
        for (int i = 0; i < highScorePanels.Length; i++)
        {
            int rank = i + 1;
            string rankString;
            switch (rank)
            {
                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
                default: rankString = rank + "th"; break;
            }
            highScorePanels[i].transform.Find("PosText").GetComponent<Text>().text = rankString;
            highScorePanels[i].transform.Find("ScoreText").GetComponent<Text>().text = "000000";
            highScorePanels[i].transform.Find("NameText").GetComponent<Text>().text = "JFSV";

        }
        UpdateHighScoreTable();
    }

    [System.Serializable]
    private class HighScores
    {
        public List<HighScoreEntry> highScoreEntriesList;
    }

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
