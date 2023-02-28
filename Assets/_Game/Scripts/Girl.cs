using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Girl : MonoBehaviour
{
    [SerializeField] float _movementTimeToReachEachWaypoint = .3f;
    [SerializeField] Animator _animator;

    
    RoadCompletionChecker _roadChecker;

    void Awake()
    {
        _roadChecker = ServiceLocator.Get<RoadCompletionChecker>();
    }

    public void HandleRoadCompletion()
    {
        var roads = _roadChecker.GetSortedRoads();
        Debug.Log(roads.Count);
        foreach (var roadPiece in roads)
        {
            Debug.Log(roadPiece.gameObject.name, roadPiece.gameObject);
        }
        StartCoroutine(Move());
        IEnumerator Move()
        {
            _animator.SetBool("isMoving", true);
            for (int i = 0; i < roads.Count; i++)
            {
                var waypoints = roads[i].GetWaypoints();
                foreach (var wp in waypoints)
                {
                    transform.DOMove(wp.position, _movementTimeToReachEachWaypoint).SetEase(Ease.Linear);
                    yield return new WaitForSecondsRealtime(_movementTimeToReachEachWaypoint + .1f);
                }
            }
            transform.DOMove(_roadChecker._endTile.transform.position, .1f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    ServiceLocator.Get<GameManager>().OnGirlReachedHome();
                    _animator.SetBool("isMoving", false);
                });
        }
    }
    
}