using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropSystem : MonoBehaviour
{
    [SerializeField] LayerMask _layerMask;
    Camera _camera;
    Draggable _currentDraggable;

    void Start()
    {
        _camera = ServiceLocator.Get<Camera>(SerLocID.mainCam);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetRay(_camera);
        
            if (Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                Draggable draggable = hitInfo.transform.GetComponentInParent<Draggable>();
                if (draggable != null)
                {
                    _currentDraggable = draggable;
                    draggable.StartDrag();
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (_currentDraggable != null)
            {
                _currentDraggable.StopDrag();
            }

            _currentDraggable = null;
        }
    }

    void FixedUpdate()
    {
        Ray ray = GetRay(_camera);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
        {
            if(_currentDraggable != null)
            {
                _currentDraggable.Drag(hitInfo);
                
                if (hitInfo.transform.GetComponentInParent<Tile>() == null)
                {
                    _currentDraggable.transform.position = hitInfo.point;
                }
            }
        }
    }

    Ray GetRay(Camera cam)
    {
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

        return ray;
    }
}
