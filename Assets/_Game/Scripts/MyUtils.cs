using UnityEngine;

public static class MyUtils
{
    public static Ray GetRayFromCamToMouse(Camera cam)
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