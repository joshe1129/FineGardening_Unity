using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;
    public Text timeCounter;
    private TimeSpan timePlaying;
    [SerializeField] private bool timerGoing;
    [SerializeField] private float elapsedTime;
    private float ResetElapsedTime;

    public static bool Paused = false;
    public GameObject GameOverUI;

    private void Awake()
    {
        ResetElapsedTime = elapsedTime;
        instance = this;
        timeCounter.text = "Time: 00:00";
        timerGoing = false;
    }  
    
    public void BeginTimer()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing && elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm':'ss");
            timeCounter.text = timePlayingStr;

            yield return null;          
        }

        if (elapsedTime <= 0f)
        {
            timerGoing = false;
            
            GameOverUI.SetActive(true);
            Time.timeScale = 0f;
            Paused = true;
            elapsedTime = ResetElapsedTime;
        }  
    }
}
