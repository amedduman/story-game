using System;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public RaycastHit? Raycast()
    {
        if (Physics.Raycast(transform.position, transform.forward,out RaycastHit hitInfo))
        {
            return hitInfo;
        }

        return null;
    }
}