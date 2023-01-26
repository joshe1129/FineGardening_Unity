using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeatherSystem : MonoBehaviour
{
    public Weather currentWeather;
    public List<Weather> weather = new List<Weather>();
    public new Light light;
    private CanvasRenderer canvasWeatherPanel;
    [SerializeField] private int index;
    [SerializeField] private Image[] weatherIcons;
    [SerializeField] List<Weather> weatherCycle = new List<Weather>();

    private void Awake()
    {
        light = GameObject.FindObjectOfType<Light>();
        canvasWeatherPanel = GameObject.Find("WeatherPanel").GetComponent<CanvasRenderer>();
        weatherIcons = canvasWeatherPanel.GetComponentsInChildren<Image>();
        WeatherCycle();
    }

    private void ChangeLight(float intesity)
    {      
        light.intensity = intesity;
        light.color = currentWeather.color;
    }
    /// <summary>
    /// crea un array de los climas que apareceran en el juego de manera aleatoria
    /// </summary>

    private void WeatherCycle()
    {
        float total = 0;
        for (int i = 0; i < weather.Count; i++)
        {
            total += weather[i].weatherChances;
        }
        for (int i = 0; i < 40; i++)
        {
            float randomNumber = Random.Range(0, total);
            for (int j = 0; j < weather.Count - 1; j++)
            {
                if (randomNumber <= weather[j].weatherChances)
                {
                    weatherCycle.Add(weather[j]);
                    j = weather.Count;
                }
                else
                {
                    randomNumber -= weather[j].weatherChances;
                }
            }
        }
        AssignCurrentWeather();
    }

    /// <summary>
    /// actualiza los sprites de la barra de clima para que simulen el cambio del clima
    /// </summary>
    void AssignCurrentWeather()
    {
        currentWeather = weatherCycle[index];
        ChangeLight(currentWeather.lighIntensity);
        if (index == 0)
        {
            weatherIcons[4].enabled = false;
            weatherIcons[5].enabled = false;
            weatherIcons[1].sprite = currentWeather.iconImage;
            weatherIcons[2].sprite = weatherCycle[index + 1].iconImage;
            weatherIcons[3].sprite = weatherCycle[index + 2].iconImage;

        }
        else if (index == 1)
        {
            weatherIcons[4].enabled = false;
            weatherIcons[1].sprite = currentWeather.iconImage;
            weatherIcons[2].sprite = weatherCycle[index + 1].iconImage;
            weatherIcons[3].sprite = weatherCycle[index + 2].iconImage;
            weatherIcons[5].enabled = true;
            weatherIcons[5].sprite = weatherCycle[index - 1].iconImage;

        }
        else
        {
            weatherIcons[4].enabled = true;
            weatherIcons[5].enabled = true;
            weatherIcons[1].sprite = currentWeather.iconImage;
            weatherIcons[2].sprite = weatherCycle[index + 1].iconImage;
            weatherIcons[3].sprite = weatherCycle[index + 2].iconImage;
            weatherIcons[4].sprite = weatherCycle[index - 2].iconImage;
            weatherIcons[5].sprite = weatherCycle[index - 1].iconImage;
        }
        StartCoroutine(ChangeWeather(currentWeather.duration));

    }

       /// <summary>
       /// Assigna chances de que aparescan en el ciclo de clima 
       /// dependiendo del tipo de clima que se busque
       /// </summary>

    public void AssingWeatherChances(Dropdown weatherType)
    {
        switch (weatherType.value)
        {
            case 0:
                weather[0].weatherChances = 90;
                weather[1].weatherChances = 80;
                weather[2].weatherChances = 50;
                weather[3].weatherChances = 20;
                weather[4].weatherChances = 5;
                break;
            case 1:
                weather[0].weatherChances = 90;
                weather[1].weatherChances = 30;
                weather[2].weatherChances = 10;
                weather[3].weatherChances = 30;
                weather[4].weatherChances = 90;
                break;
            case 2:
                weather[0].weatherChances = 5;
                weather[1].weatherChances = 90;
                weather[2].weatherChances = 80;
                weather[3].weatherChances = 70;
                weather[4].weatherChances = 5;
                break;
            default:
                weather[0].weatherChances = 50;
                weather[1].weatherChances = 50;
                weather[2].weatherChances = 50;
                weather[3].weatherChances = 50;
                weather[4].weatherChances = 50;
                break;
        }

    }    

    IEnumerator ChangeWeather(float duration)
    {
        yield return new WaitForSeconds(duration);
        index++;
        AssignCurrentWeather();
    }
}

[System.Serializable]
public class Weather
{
    public string name;
    public float duration;
    public int sunAmount;
    public int waterAmount;
    public Color color;
    public float lighIntensity;
    public Sprite iconImage;
    public int weatherChances;
}
