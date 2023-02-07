using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropSystem : MonoBehaviour
{
    [SerializeField] Draggable _draggable;
    void FixedUpdate()
    {
        Vector3 mouseScreenPos = new Vector3(Input.mousePosition.x, 0, Input.mousePosition.y);
        Vector3 mouseWorldPos = ServiceLocator.Get<Camera>(SerLocID.mainCam).ScreenToWorldPoint(mouseScreenPos);
        _draggable.transform.position = mouseWorldPos;
        Debug.Log(mouseWorldPos);
    }
}
