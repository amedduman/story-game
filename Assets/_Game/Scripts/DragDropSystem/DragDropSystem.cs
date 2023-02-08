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
            Ray ray = MyUtils.GetRayFromCamToMouse(_camera);
        
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
                _currentDraggable.Drop();
            }

            _currentDraggable = null;
        }
    }

    void FixedUpdate()
    {
        Ray ray = MyUtils.GetRayFromCamToMouse(_camera);
        
        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, _layerMask))
        {
            if(_currentDraggable != null)
            {
                _currentDraggable.Drag(hitInfo);
            }
        }
    }

    
}
