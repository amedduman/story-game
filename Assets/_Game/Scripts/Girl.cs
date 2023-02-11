using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Girl : MonoBehaviour
{
    void OnEnable()
    {
        ServiceLocator.Get<RoadCompletionChecker>().OnRoadComplete += HandleRoadCompletion;
    }

    void OnDisable()
    {
        ServiceLocator.Get<RoadCompletionChecker>().OnRoadComplete -= HandleRoadCompletion;
    }
    void HandleRoadCompletion()
    {
        var roads = ServiceLocator.Get<RoadCompletionChecker>().GetSortedRoads();
        StartCoroutine(Move());
        IEnumerator Move()
        {
            for (int i = 0; i < roads.Count; i++)
            {
                var roadRnds = roads[i].GetComponentsInChildren<Renderer>();
                foreach (var rnd in roadRnds)
                {
                    // transform.position = rnd.transform.position;
                    transform.DOMove(rnd.transform.position, .1f);
                    yield return new WaitForSecondsRealtime(.12f);
                }
            }
            transform.DOMove(ServiceLocator.Get<RoadCompletionChecker>()._endTile.transform.position, .1f);
        }
    }
    
}