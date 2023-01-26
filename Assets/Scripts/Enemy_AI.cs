using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AI : MonoBehaviour
{   /// <summary>
/// por ahora solo es un prototipo que llega a instanciar una flor tuve que modificar los permisos de algunos atributos
/// en otros scripts para que funcione
/// </summary>
/*
    [SerializeField] protected int Money;
    [SerializeField] private int numberofFlowers;
    public int score;
    [SerializeField] private FlowerPot[] flowerPots;
    [SerializeField] private GardenManager gardenManager;
    [SerializeField] private StoreManager storeManager;
    [SerializeField] private WeatherSystem weatherSystem;
    [SerializeField] private GameObject bestPlantoPlant;
    public FlowerScriptableObject[] flowers;

    protected delegate void GardenManagerDelegate();
    protected static GardenManagerDelegate gardenManagerDelegate;

    private void Awake()
    {
        flowerPots = FindObjectsOfType<FlowerPot>();
        gardenManager = FindObjectOfType<GardenManager>();
        storeManager = FindObjectOfType<StoreManager>();
        weatherSystem = FindObjectOfType<WeatherSystem>();
    }
    private void Start()
    {
        GetListofSeeds();
        GetBestFlowertoPlant();
        PlantFlower();
       
    }
    /*
    private void Update()
    {
        gardenManagerDelegate?.Invoke();
    }
    */
    /*
    private void GetListofSeeds()
    {
        flowers = storeManager.seedsObjects.ToArray();
      
    }

    void PlantFlower()
    {
        for (int i = 0; i < flowerPots.Length; i++)
        {
            if (!flowerPots[i].isCovered && !flowerPots[i].isPlanted)
            {
                Instantiate(bestPlantoPlant, flowerPots[i].transform.position, Quaternion.identity);
                i = flowerPots.Length;
            }
        }

        Money -= flowers[0].buyPrice;
        Debug.Log("lalala");
    }

    void GetBestFlowertoPlant()
    {
        for (int i = 0; i < storeManager.seedsObjects.Count; i++)
        {

            if ((weatherSystem.currentWeather.sunAmount == storeManager.seedsObjects[i].sunNeeded) ||
                (weatherSystem.currentWeather.sunAmount + 1 == storeManager.seedsObjects[i].sunNeeded) ||
                (weatherSystem.currentWeather.sunAmount - 1 == storeManager.seedsObjects[i].sunNeeded)
                )
            {
                Debug.Log(weatherSystem.currentWeather.name + " + " + weatherSystem.currentWeather.sunAmount + " + " + storeManager.seedsObjects[i] + " + " + storeManager.seedsObjects[i].sunNeeded);
                bestPlantoPlant = storeManager.seedsObjects[i].FlowerStages[0];
            }
        }

    }
    */
}


