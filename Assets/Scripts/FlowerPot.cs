using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPot : MonoBehaviour
{
    public GameObject flowerPlanted;
    public bool isPlanted = false;
    private FlowerScriptableObject selectedFlower;
    private GardenManager gardenManager;
    

    public bool isCovered;

    private void Start()
    {
        gardenManager = FindObjectOfType<GardenManager>();
    }

    private void Update()
    {
        if (GetComponentInChildren<FlowerNeeds>())
        {
            isPlanted = true;
        }
        else
            isPlanted = false;  
    }

    //Las plantas instancean como hijos del flowerpot para poder acceder a sus atributos de forma mas rapida y simple
    private void OnMouseDown()
    {
        if (isPlanted && GetComponentInChildren<FlowerNeeds>())
        {
            if (GetComponentInChildren<FlowerNeeds>().isMature) //&& !gardenManager.isPlanting
            {
                Harvest();
                isPlanted = false;
            }
        }
        else if (gardenManager.isPlanting && gardenManager.selectFlower.Seed.buyPrice <= gardenManager.money)
        {
            Plant(gardenManager.selectFlower.Seed);       
        }
    }

    private void OnMouseOver()
    {     
        if (gardenManager.isPlanting)
        {
            if (isPlanted || gardenManager.selectFlower.Seed.buyPrice > gardenManager.money)
            {
                var outline = GetComponent<Outline>();
                if (outline != null)
                {
                    outline.OutlineWidth = 5;
                    outline.OutlineColor = Color.red;
                }
            }
            else if(!isPlanted)
            {
                var outline = GetComponent<Outline>();
                if (outline != null)
                {
                    outline.OutlineWidth = 5f;
                    outline.OutlineColor = Color.green;
                }
            }
        }
    }

    private void OnMouseExit()
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = 0f;
        }
    }

    /// <summary>
    /// Función que permite la recolección de las flores, Actualiza el estado del terreno y del dinero que nos queda luego de la transacción
    /// </summary>
    private void Harvest()
    {
        Debug.Log("Harvested");
        isPlanted = false;
        gardenManager.Transaction(+selectedFlower.sellPrice);
        gardenManager.ScorePoints(+selectedFlower.scorePointsHarvest);
        Destroy(GetComponentInChildren<FlowerNeeds>().gameObject);
        flowerPlanted = null;
        gardenManager.isPlanting = false; 
        gardenManager.selectFlower = null; 
    }

    /// <summary>
    /// Al Clickear sobre el terreno, Se instancia la semilla de la flor. A su vez actualizamos la cantidad de dinero que nos queda despues de la transaccion.
    /// Update Jose 4/07/2021: volvi publica la funcion para que la ia trabaje con ella
    /// </summary>
    /// <param name="newFlower">Tipo de semilla selecionada que iremos a plantar</param>
    public void Plant(FlowerScriptableObject newFlower)
    {
        selectedFlower = newFlower;
        isPlanted = true;
        gardenManager.Transaction(-selectedFlower.buyPrice);
        gardenManager.ScorePoints(+selectedFlower.scorePointsPlant);
        //selectedFlower.FlowerStages.transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
        flowerPlanted = Instantiate(selectedFlower.FlowerStages, transform.position, selectedFlower.FlowerStages.transform.rotation);//instancia la semilla
        gardenManager.isPlanting = false; 
        gardenManager.selectFlower = null; 
    }

    /// <summary>
    /// Al replantar una planta actualiza las variables que se usan en este script
    /// </summary>
    public void RePlantFlower(GameObject newFlower)
    {
        isPlanted = true;
        flowerPlanted = newFlower;
        selectedFlower = newFlower.GetComponent<FlowerNeeds>().scriptableFlowerObject;
        newFlower.transform.SetParent(this.transform);
    }
}
