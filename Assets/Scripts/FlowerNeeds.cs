using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerNeeds : MonoBehaviour
{
    [SerializeField] private WeatherSystem weatherSystem;
    [SerializeField] private Image[] neededImage;
    [SerializeField] private int currentWater;
    [SerializeField] private int currentSun;
    [SerializeField] private bool canGrowth;
    [SerializeField] private bool sunOK;
    [SerializeField] private bool waterOk;
    [SerializeField] private bool alreadyinDeadCooldown;
    [SerializeField] private bool youCanWaterthePlant;
    public bool isMature;
    [SerializeField] private bool plantWillDie;
    [SerializeField] private GameObject nextStage;
    [SerializeField] public GameObject currentPot;
    [SerializeField] private float closestPot;
    [SerializeField] private LayerMask WhatIsPot;
    [SerializeField] private Camera lookCamera;
    public FlowerScriptableObject scriptableFlowerObject;

    private void Start()
    {
        gameObject.transform.SetParent(NearestFlowerPot().transform);
        lookCamera = FindObjectOfType<Camera>();
        currentPot = NearestFlowerPot();
        weatherSystem = FindObjectOfType<WeatherSystem>();
        currentWater = weatherSystem.currentWeather.waterAmount;
        currentSun = weatherSystem.currentWeather.sunAmount;
        youCanWaterthePlant = true;
        neededImage = GetComponentsInChildren<Image>();
        plantWillDie = false;
        if (isMature)
        {
            currentSun = scriptableFlowerObject.sunNeeded;
            currentWater = scriptableFlowerObject.waterNeeded;
            plantWillDie = false;
            neededImage[4].enabled = true;
            neededImage[4].transform.LookAt(lookCamera.transform);
        }
        else
        {
            checkNeeds();
            StartCoroutine(Growing());
        }
    }

    /// <summary>
    /// revisa las necesidades de la planta
    /// </summary>
    public void checkNeeds()
    {
        CheckWaterNeeded();
        CheckSunNeeded();
        if (plantWillDie || isMature)
        {
            return;
        }
        else if (!isMature)// si la planta esta en etapa madura no hay necesidad de revisar sus necesidades
        {
            if (waterOk && sunOK)
            {
                canGrowth = true;
                if (alreadyinDeadCooldown)
                {
                    StopCoroutine(DeadTimer());
                    alreadyinDeadCooldown = false;
                }
            }
            else
                canGrowth = false;
        }
    }

    /// <summary>
    /// Revisa la cantidad de agua que necesita la planta y actualiza sus advertencias
    /// </summary>
    private void CheckWaterNeeded()
    {
        if (currentWater == 0)
        {
            currentPot = NearestFlowerPot();
            if (currentPot.GetComponent<FlowerPot>().isCovered)
            {
                currentWater = 0;
            }
            else
            {
                currentWater = weatherSystem.currentWeather.waterAmount;
            }
        }

        if (currentWater < scriptableFlowerObject.waterNeeded)
        {
            waterOk = false;
            if (youCanWaterthePlant)
            {
                neededImage[0].enabled = true;
                neededImage[0].transform.LookAt(lookCamera.transform);
            }
            if (!alreadyinDeadCooldown)// si la planta ya tiene un temporizador de muerte no vuelve a llamar a la funcion
            {
                StartCoroutine(DeadTimer());
            }
        }
        else
        {
            waterOk = true;
            youCanWaterthePlant = false;
        }
        if (sunOK && waterOk)
        {
            StopCoroutine(DeadTimer());
        }
    }

    /// <summary>
    /// Revisa la cantidad de luz que esta recibiendo la planta y actualiza advertencias
    /// </summary>
    private void CheckSunNeeded()
    {
        currentPot = NearestFlowerPot();
        if (currentPot.GetComponent<FlowerPot>().isCovered)
        {
            currentSun = 2;
        }
        else
        {
            currentSun = weatherSystem.currentWeather.sunAmount;
        }

        if (currentSun == scriptableFlowerObject.sunNeeded || currentSun == scriptableFlowerObject.sunNeeded + 1 || currentSun == scriptableFlowerObject.sunNeeded - 1)
        {
            neededImage[1].enabled = false;
            neededImage[2].enabled = false;
            sunOK = true;

        }
        else if (currentSun > scriptableFlowerObject.sunNeeded + 1)
        {
            sunOK = false;
            neededImage[2].enabled = true;
            neededImage[2].transform.LookAt(lookCamera.transform);
            if (!alreadyinDeadCooldown)// si la planta ya tiene un temporizador de muerte no vuelve a llamar a la funcion
            {
                StartCoroutine(DeadTimer());
            }
        }
        else if (currentSun < scriptableFlowerObject.sunNeeded - 1)
        {
            sunOK = false;
            neededImage[1].enabled = true;
            neededImage[1].transform.LookAt(lookCamera.transform);
            if (!alreadyinDeadCooldown)
            {
                StartCoroutine(DeadTimer());
            }
        }
        else
        {
            Debug.Log("current sun= " + currentSun + "needed sun= " + scriptableFlowerObject.sunNeeded);
        }
        if (sunOK && waterOk)
        {
            StopCoroutine(DeadTimer());
        }
    }

    /// <summary>
    /// Funcion que iniciara un temporizador de muerte en las plantas 
    /// si tras ese tiempo no se cumplen las necesidades la planta no podra crecer
    /// y pasara a estado de muerte
    /// </summary>
    IEnumerator DeadTimer()
    {
        alreadyinDeadCooldown = true;
        yield return new WaitForSeconds(10);
        alreadyinDeadCooldown = false;
        checkNeeds();
        if (waterOk && sunOK)
        {
            Debug.Log("planta vivira");
            youCanWaterthePlant = false;
            plantWillDie = false;
        }
        else
        {
            Debug.Log("planta morira");
            plantWillDie = true;
            canGrowth = false;
            youCanWaterthePlant = false;
        }
    }

    /// <summary>
    /// Funcion para regar la planta y controlar su riego o
    /// eliminar la planta si esta en estado de muerte
    /// </summary>
    private void OnMouseDown()
    {
        if (youCanWaterthePlant && (scriptableFlowerObject.waterNeeded > currentWater))
        {
            currentWater++;
            neededImage[0].enabled = false;
            youCanWaterthePlant = false;
            if (!waterOk)
            {
                StopCoroutine(DeadTimer());
                StartCoroutine(CooldownWater());
            }
        }
        else if (plantWillDie)
        {
            Destroy(gameObject);
            currentPot.GetComponent<FlowerPot>().isPlanted = false;
        }
    }
    private void OnMouseUp()
    {
        checkNeeds();
    }
    

    IEnumerator CooldownWater()
    {
        yield return new WaitForSeconds((scriptableFlowerObject.timeBetweenStages) / (Mathf.Abs(weatherSystem.currentWeather.waterAmount - scriptableFlowerObject.waterNeeded)));
        youCanWaterthePlant = true;
        checkNeeds();
    }
    /// <summary>
    /// funcion que controla el crecimiento  
    /// y evita conflictos al replantear la planta a un nuevo pot.
    /// </summary>
    IEnumerator Growing()
    {
        yield return new WaitForSeconds(scriptableFlowerObject.timeBetweenStages);
        checkNeeds();
        if (canGrowth)
        {
            StopCoroutine(DeadTimer());
            Instantiate(nextStage, NearestFlowerPot().transform.position, Quaternion.Euler(0, -180, 0));
            Destroy(gameObject);
        }
        else
        {
            plantWillDie = true;
            neededImage[3].enabled = true;
            neededImage[3].transform.LookAt(lookCamera.transform);
            StopAllCoroutines();
        }
    }
    /// <summary>
    /// funcion que busca el pot mas cercano para poder asignarlo como padre, poder hacer referencia a sus variables
    /// y funciones del script FlowePot
    /// </summary>
    public GameObject NearestFlowerPot()
    {
        GameObject nearestPot = null;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, closestPot, WhatIsPot);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject != gameObject && hitColliders[i].GetComponent<FlowerPot>())
            {
                if (!nearestPot)
                {
                    nearestPot = hitColliders[i].gameObject;
                }
                else
                {
                    if (Vector3.Distance(nearestPot.transform.position, transform.position) > Vector3.Distance(hitColliders[i].transform.position, transform.position))
                    {
                        nearestPot = hitColliders[i].gameObject;
                    }
                }
            }
        }
        return nearestPot;
    }
}
