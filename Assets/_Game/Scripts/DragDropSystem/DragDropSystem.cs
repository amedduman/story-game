using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropSystem : MonoBehaviour
{
    [SerializeField] Draggable _draggable;
    void FixedUpdate()
    {
        Camera cam = ServiceLocator.Get<Camera>(SerLocID.mainCam);
        Ray ray;
        
        if (cam.orthographic)
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = cam.nearClipPlane;
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
            ray = new Ray(mouseWorldPos, cam.transform.forward);
        }
        else
        {
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = cam.nearClipPlane;
            Vector3 mouseWorldPos = cam.ScreenToWorldPoint(mouseScreenPos);
            ray = new Ray(cam.transform.position, mouseWorldPos - cam.transform.position);
        }
        
        Debug.DrawRay(ray.origin, ray.direction * 100, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            _draggable.transform.position = hitInfo.point;
        }
    }
}
