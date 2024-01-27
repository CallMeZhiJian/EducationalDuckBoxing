using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithMouseDrag : MonoBehaviour
{
    private Camera mainCamera;
    private float cameraZDistance;

    private Vector3 initialPos;

    private void Start()
    {
        initialPos = transform.localPosition;
        mainCamera = Camera.main;
        cameraZDistance = mainCamera.WorldToScreenPoint(transform.position).z;
    }

    private void OnMouseUp()
    {
        transform.localPosition = initialPos;
        //transform.hasChanged = false;
    }

    private void OnMouseDrag()
    {
        Vector3 ScreenPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraZDistance);
        Vector3 NewWorldPosition = mainCamera.ScreenToWorldPoint(ScreenPosition);
        transform.position = NewWorldPosition;
    }
}
