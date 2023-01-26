using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GardenManager : MonoBehaviour
{
    public SeedItem selectFlower;
    public bool isPlanting = false;
    public int money = 100;

    private int ReMoney;
    public int score = 0;
    public int highScore;

    public Text moneyText;
    public Text nameText;
    public Text scoreText;
    public Text gameOverScoreText;
    public Text gameOverHighScoreText;

    public bool gamePlaying = false;

    public bool isSelecting = false;
    public int selectedTool = 0;

    public GameObject GameOverUI;
    [SerializeField] private HighScoreTable highScoreTable;

    private void Awake()
    {
        ReMoney = money;
        gameOverHighScoreText.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    // Asignando la cantidad de dinero que tendremos al comienzo de la partida;
    private void Start()
    {     
        moneyText.text = "$" + money;
        scoreText.text = "Score: " + score;
    }

    // Acciones de seleccionar y deseleccionar la semilla que estamos por comprar
    public void SelectFlower(SeedItem newSeed)
    {
        if (selectFlower == newSeed)
        {
            CheckSelection();
            
        }
        else
        {
            selectFlower = newSeed;
            isPlanting = true;            
        }  
    }

    public void SelectTool(int toolNumber)
    {
        if(toolNumber == selectedTool)
        {
            //Deselecciona
            CheckSelection();
        }
        else
        {
            //Selescciona la herramienta
            CheckSelection();
            isSelecting = true;
            selectedTool = toolNumber;
        }
    }

    private void CheckSelection()
    {
        if (isPlanting)
        {
            isPlanting = false;
            selectFlower = null;
        }

        if (isSelecting)
        {
            isSelecting = false;
            selectedTool = 0;
        }
    }

    //Funcion que actualiza nuestro presupuesto dependiendo de la compra y venta. Este se actualiza en la clase "FlowerPot"
    public void Transaction(int value)
    {
        money += value;
        moneyText.text = "$ " + money;
    }

    public void ScorePoints(int value)
    {
        score += value;
        scoreText.text = "Score: " + score;
        gameOverScoreText.text = score.ToString();
        UpdateHighScore();
    }

    public void BeginGame()
    {
        gamePlaying = true;
        TimeController.instance.BeginTimer();
    }   

    public void UpdateHighScore()
    {
        highScore = score;

        if (highScore > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", highScore);
            gameOverHighScoreText.text = "(High Score: " + highScore + ")";
        }
    }

    /// <summary>
    /// Reinicia el juego sin cambiar de escena, se eliminan todas las flores del campo, se actualiza el estado del terreno, puntos, dinero y tiempo
    /// </summary>
    public void TryEgain()
    {
        FlowerNeeds[] potsall = FindObjectsOfType<FlowerNeeds>();
        for (int i = 0; i < potsall.Length; i++)
        {
            Destroy(potsall[i].gameObject);
        }

        FlowerPot[] isPlantedAll = FindObjectsOfType<FlowerPot>();
        for (int i = 0; i < isPlantedAll.Length; i++)
        {
            isPlantedAll[i].isPlanted = false;
        }

        moneyText.text = "$" + ReMoney;
        money = ReMoney;
        ResetScore();
        

        GameOverUI.SetActive(false);

        Time.timeScale = 1f;
        TimeController.instance.BeginTimer();
    }

    public void ResetScore()
    {
        score = 0;
        scoreText.text = "Score: " + score;
    }

    public void ReStartScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScoreTable.ResetLeaderBoard();
        gameOverHighScoreText.text = "(High Score: 0)";
    }


}
