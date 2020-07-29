using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float autoRotSpeed = 0.5f;
    public float rotSpeed = 20;
    private bool dragging = false;

    private void Update()
    {
        Drag();
    }

    private void OnMouseDrag()
    {
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
    }

    private void Drag()
    {
        if (dragging)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;

            transform.Rotate(Vector3.up, -rotX, Space.Self);

        }
        else
        {
            transform.Rotate(Vector3.up, -autoRotSpeed, Space.Self);
        }
    }
}
