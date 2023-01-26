using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class DisplayUI : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    private Text tooltipText;
    private RectTransform backgroundRectTransform;
    private static DisplayUI instanceUI;

    private void Awake()
    {
        instanceUI = this;
        backgroundRectTransform = transform.Find("BGTooltip").GetComponent<RectTransform>();
        tooltipText = transform.Find("TxtTooltip").GetComponent<Text>();

        //ShowTooltip("Random tooltip text");
        HideTooltip();
    }

    private void Update()
    {
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out localPoint);
        transform.localPosition = localPoint;
    }

    private void ShowTooltip(string tooltipString)
    {
        gameObject.SetActive(true);

        tooltipText.text = tooltipString;
        float textPaddingSize = 12f;
        Vector2 backgroundSize = new Vector2(tooltipText.preferredWidth + textPaddingSize * 2f, tooltipText.preferredHeight + textPaddingSize * 2f);
        backgroundRectTransform.sizeDelta = backgroundSize;
        Update();
    }

    private void HideTooltip()
    {
        gameObject.SetActive(false);
    }



    public static void ShowTooltip_Static(string tooltipString)
    {
        instanceUI.ShowTooltip(tooltipString);
    }

    public static void HideTooltio_Static()
    {
        instanceUI.HideTooltip();
    }

}
