using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedItem : MonoBehaviour
{
    public FlowerScriptableObject Seed;
    public Text nameTxt;
    public Text factionTxt;
    public Text priceTxt;
    public Image icon;
    private GardenManager gardenManager;

    void Start()
    {
        gardenManager = FindObjectOfType<GardenManager>();
        InitializeUI();
    }

    // Funcion que es llamada del Boton "Buy Button" dentro de la UI, actualiza la semilla que hemos selecionado para ser plantada.
    public void BuySeed()
    {
        gardenManager.SelectFlower(this);
    }

    // Actualiza los valores iniciales de cada tipo de semilla que tenemos en la UI, esto dependera de los valores asignados en los scriptableObjects
    void InitializeUI()
    {
        nameTxt.text = Seed.FlowerType;
        factionTxt.text = "(" + Seed.Faction + ")";
        priceTxt.text = "$" + Seed.buyPrice;
        icon.sprite = Seed.icon;
    }
}
