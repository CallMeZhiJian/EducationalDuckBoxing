using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StretchMouseDrag : MonoBehaviour
{
    public Transform worldAnchor;

    private Camera mainCamera;
    private float cameraZDistance;
    private Vector3 intialScale;
    private Vector3 mousePos;

    private void Start()
    {
        mainCamera = Camera.main;
        intialScale = transform.localScale;
        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(mousePos);

            transform.position = mousePos;
        }
    }

    private void OnMouseDrag()
    {
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = mousePos;
        //Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
        //mousePos = mainCamera.ScreenToWorldPoint(mouseScreenPos);

        //float distance = Vector3.Distance(worldAnchor.position, mousePos);
        //transform.localScale = new Vector3(intialScale.x, distance / 2f, intialScale.z);

        //Vector3 middlePoint = (worldAnchor.position + mousePos) / 2f;
        //transform.position = middlePoint;

        //Vector3 rotationDirection = (mousePos - worldAnchor.position);
        //transform.up = rotationDirection;
    }
}
