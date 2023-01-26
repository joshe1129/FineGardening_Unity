using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Library_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action MouseOverOnceFunc = null;
    public Action MouseOutOnceFunc = null;

    private void Start()
    {
        var outline = GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = 1f;
            outline.OutlineColor = Color.green;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MouseOverOnceFunc != null) MouseOverOnceFunc();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MouseOutOnceFunc != null) MouseOutOnceFunc();
    }

    private void OnMouseOver()
    {
        if (MouseOverOnceFunc != null) MouseOverOnceFunc();
    }

    private void OnMouseExit()
    {
        if (MouseOutOnceFunc != null) MouseOutOnceFunc();
    }

}

