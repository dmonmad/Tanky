using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour
{
    public float rotSpeed = 20;

    void OnMouseDrag()
    {
        Debug.Log("DRAGING");
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;

        transform.Rotate(Vector3.up, -rotX, Space.Self);
    }
}
