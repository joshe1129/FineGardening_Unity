using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    public GameObject seedItem;
    List<FlowerScriptableObject> seedsObjects = new List<FlowerScriptableObject>();

    /// <summary>
    /// Toma todos los ScriptableObjects de la carpeta Resources, las ordena del menor precio al mayou y luego Instancia los prefabs de semillas que van en el "StoreSeeeds"
    /// </summary>
    private void Awake()
    {
        var loadFlower = Resources.LoadAll("ScriptableSeeds", typeof(FlowerScriptableObject));

       
        int count = 0;
        while (count < 6)
        {
            int randomIndex = Random.Range(0, loadFlower.Length);
            if (!seedsObjects.Contains((FlowerScriptableObject)loadFlower[randomIndex]))
            {
                seedsObjects.Add((FlowerScriptableObject)loadFlower[randomIndex]);
                count++;
            }
        }
        seedsObjects.Sort(SortByPrice);

        foreach (var flower in seedsObjects)
        {
            SeedItem newPlant = Instantiate(seedItem, transform).GetComponent<SeedItem>();
            newPlant.Seed = (FlowerScriptableObject)flower;        
        }  
    }

    int SortByPrice(FlowerScriptableObject seedObject1, FlowerScriptableObject seedObject2)
    {
        return seedObject1.buyPrice.CompareTo(seedObject2.buyPrice);
    }
}
