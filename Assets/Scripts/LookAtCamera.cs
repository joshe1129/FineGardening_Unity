using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cameraPosition;

    private void Start()
    {
        cameraPosition = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(cameraPosition);
    }
}
