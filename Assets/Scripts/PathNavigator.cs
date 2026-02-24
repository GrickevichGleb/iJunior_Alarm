using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Mover))]
public class PathNavigator : MonoBehaviour
{
    [SerializeField] private Transform _path;

    private WayPoint[] _wayPoints;
    private Transform _currentWayPoint;
    private int _currentWayPointInd = 0;
    private Mover _mover;
    private bool _isForwardMove = true;

    private void Awake()
    {
        _mover = gameObject.GetComponent<Mover>();
    }

    private void Start()
    {
        _wayPoints = _path.GetComponentsInChildren<WayPoint>();
        _mover.SetDestination(_wayPoints[_currentWayPointInd].transform);
    }

    private void Update()
    {
        if (_mover.HasReachedDestination())
        {
            SetNextWayPointInd();
            _mover.SetDestination(_wayPoints[_currentWayPointInd].transform);
        }
    }

    private void SetNextWayPointInd()
    {
        int newIndex = 0;
        
        if (_isForwardMove)
        {
            newIndex = _currentWayPointInd + 1;

            if (newIndex == _wayPoints.Length)
                _isForwardMove = false;
        }

        if (_isForwardMove == false)
        {
            newIndex = _currentWayPointInd - 1;

            if (newIndex == 0)
                _isForwardMove = true;
        }

        _currentWayPointInd = newIndex;
    }
}
