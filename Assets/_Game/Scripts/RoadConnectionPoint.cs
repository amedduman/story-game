using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class RoadConnectionPoint : MonoBehaviour
{
     [SerializeField] RoadConnectionPoint _peerPoint;
     [SerializeField] LayerMask _layerMask;
     float _radius = .1f;

     [CanBeNull]
     public RoadConnectionPoint GetConnectionPoint()
     {
          var hitInfo = SphereCast();
          foreach (var hit in hitInfo)
          {
               var rp = hit.transform.GetComponent<RoadConnectionPoint>();
               if (rp != null)
               {
                    if (rp.gameObject != gameObject)
                    {
                         Debug.Log(rp.gameObject.name, rp.gameObject);
                         return rp;
                    }
               }
          }

          return null;
     }

     [ContextMenu("get connection")]
     public void Debug_GetConnection()
     {
          GetConnectionPoint();
     }
     
     public bool IsStartPoint()
     {
          var hitInfo = SphereCast();
          foreach (var hit in hitInfo)
          {
               if (hit.transform.CompareTag("Player"))
               {
                    return true;
               }
          }

          return false;
     }

     public bool IsEndPoint()
     {
          var hitInfo = SphereCast();
          foreach (var hit in hitInfo)
          {
               if (hit.transform.CompareTag("Finish"))
               {
                    return true;
               }
          }

          return false;
     }

     RaycastHit[] SphereCast()
     {
          var hitInfo =
               Physics.SphereCastAll(transform.position, _radius, transform.forward, Mathf.Infinity, _layerMask);
          return hitInfo;
     }
     
     

     RoadConnectionPoint GetPeerPoint()
     {
          return _peerPoint;
     }

     void OnDrawGizmosSelected()
     {
          Gizmos.DrawSphere(transform.position, _radius);
     }
}