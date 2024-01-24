using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicScaler : MonoBehaviour
{
    public float scaleFactor = 1.0f; // Adjust this to control the scaling effect

    private Vector3 initialScale;

    void Start()
    {
        // Save the initial scale of the object
        initialScale = new Vector3(0.3f, 0.3f, 0.3f);
    }

    void Update()
    {
        // Calculate the distance between the object and the camera
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        // Adjust the scale based on the distance
        transform.localScale = initialScale * (1 + scaleFactor * distance);
        Debug.Log(transform.localScale);
    }
}
