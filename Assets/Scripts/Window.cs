using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MonoBehaviour
{
    public GameObject PotText;
    public GameObject FlowerText;



    private void Start()
    {
        PotText.GetComponent<Library_UI>().MouseOverOnceFunc = () => DisplayUI.ShowTooltip_Static("Cuando la Flor este madura,\nUna moneda aparecera sobre ella,\nPara vender la flor deberas hacer\nclick sobre el terreno");
        PotText.GetComponent<Library_UI>().MouseOutOnceFunc = () => DisplayUI.HideTooltio_Static();
        
        FlowerText.GetComponent<Library_UI>().MouseOverOnceFunc = () => DisplayUI.ShowTooltip_Static("Una Flor tiene muchas necesidades\nEsta te mostrara un icono sobre ella\nSi es que necesita algo\nRiegala o transplantala si es necesario");
        FlowerText.GetComponent<Library_UI>().MouseOutOnceFunc = () => DisplayUI.HideTooltio_Static();
        
    }
}
