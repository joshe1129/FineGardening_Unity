using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static float volume = 0.5f;

    private void OnEnable()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Awake()
    {
        Volume(volume);
    }

    public static void Volume(float vol)
    {
        foreach (AudioSource audioS in GameObject.FindObjectsOfType<AudioSource>())
        {
            audioS.volume = vol;
        }
        volume = vol;
    }

    public static void Resolution(Dropdown resolution)
    {
        switch (resolution.value) // 16:9
        {
            case 0:
                Screen.SetResolution(1920, 1080, true); //Symbian3 devices like Nokia C7
                break;
            case 1:
                Screen.SetResolution(854, 480, true); //Android devices
                break;
            case 2:
                Screen.SetResolution(1136, 640, true); //iPhone 5
                break;
            case 3:
                Screen.SetResolution(1280, 720, true); //HTC One Mini, Samsung Galaxy S3
                break;
            case 4:
                Screen.SetResolution(1334, 750, true); //iPhone 6/6S, iPhone 7/7S
                break;
            case 5:
                Screen.SetResolution(1920, 1080, true); //iPhone 6 Plus, iPhone 7 Plus, Google Nexus 5, Samsung Galaxy S5
                break;
            case 6:
                Screen.SetResolution(2560, 1440, true); //Samsung Galaxy S7
                break;
        }
    }

    public void NewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
				Application.Quit();
        #endif
    }

}
