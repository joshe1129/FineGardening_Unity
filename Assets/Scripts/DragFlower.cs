using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragFlower : MonoBehaviour
{
    private bool isMouseDrag = false;
    private GameObject target;
    private Vector3 offset;
    private Vector3 screenPosition;
    private Vector3 initialPosition;
    
    [SerializeField] private LayerMask WhatIsPot;
    [SerializeField] private float Closest;

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void OnMouseDown()
    {
        initialPosition = transform.position;
        RaycastHit hitInfo;
        target = ReturnClickedObject(out hitInfo);
        if (target != null)
        {
            isMouseDrag = true;
            screenPosition = Camera.main.WorldToScreenPoint(target.transform.position);
            offset = target.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y + 1f, screenPosition.z));
        }
    }

    private void OnMouseDrag()
    { 
        if (isMouseDrag)
        {
            Vector3 currentScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPosition.z);
            Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenSpace) + offset;
            target.transform.position = new Vector3(currentPosition.x, target.transform.position.y, currentPosition.z);  
        }
    }

    private void OnMouseUp()
    {
        if (Mathf.Abs(target.transform.position.x - NearestFlowerPot().transform.position.x) <= 0.5f &&
            Mathf.Abs(target.transform.position.z - NearestFlowerPot().transform.position.z) <= 0.5f
            && !NearestFlowerPot().transform.GetComponentInChildren<FlowerNeeds>())//evita que la planta se pueda replantar encima de otra
        {
            target.transform.position = NearestFlowerPot().transform.position;
            NearestFlowerPot().gameObject.GetComponent<FlowerPot>().RePlantFlower(target);
            isMouseDrag = false;
            target.GetComponent<FlowerNeeds>().checkNeeds();//Checkea las necesidades cuado se mueve la planta
        }
        else
        {
            target.transform.position = new Vector3(initialPosition.x, initialPosition.y, initialPosition.z);
        }
    }

    GameObject ReturnClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (hit.collider.CompareTag("Draggable"))
            {
                target = hit.collider.gameObject;
            }
            else
            {
                target = null;
            }      
        }
        return target;
    }

    Transform NearestFlowerPot()
    {
        Transform nearestPot = null;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Closest, WhatIsPot);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].GetComponent<FlowerPot>())
            {
                if (!nearestPot)
                {
                    nearestPot = hitColliders[i].transform;
                }
                else
                {
                    if (Vector3.Distance(nearestPot.position, transform.position) > Vector3.Distance(hitColliders[i].transform.position, transform.position))
                    {
                        nearestPot = hitColliders[i].transform;
                    }
                }
            }
        }
        return nearestPot;
    }
}
