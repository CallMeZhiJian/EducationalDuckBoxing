using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleWithPivot : MonoBehaviour
{
    public GameObject startObject;
    public GameObject endObject;
    private Vector3 initialScale;

    private void Start()
    {
        initialScale = transform.localScale;
        //UpdateTransformForScale();
    }

    private void Update()
    {
        if (startObject.transform.hasChanged)
        {
            UpdateTransformForScale();
        }
    }

    void UpdateTransformForScale()
    {
        float distance = Vector3.Distance(startObject.transform.position, endObject.transform.position);
        transform.localScale = new Vector3(initialScale.x, distance, initialScale.z);
        
        Vector3 middlePoint = (startObject.transform.position + endObject.transform.position) / 2f;
        transform.position = middlePoint;
        
        Vector3 rotationDirection = (endObject.transform.position - startObject.transform.position);
        transform.up = rotationDirection;  
    }
}
