using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    [SerializeField] Collider _col;

    public void StartDrag()
    {
        _col.enabled = false;
    }

    public void Drag(RaycastHit hitInfo)
    {
        transform.position = hitInfo.transform.position;
    }

    public void StopDrag()
    {
        _col.enabled = true;
    }
}
