using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Flower", menuName = "Flower")]
public class FlowerScriptableObject : ScriptableObject
{
    public string FlowerType;
    public string Faction;
    public GameObject FlowerStages;
    public float timeBetweenStages;
    public int buyPrice;
    public int sellPrice;
    public int scorePointsPlant;
    public int scorePointsHarvest;
    public Sprite icon;
    public int sunNeeded;
    public int waterNeeded;
}
