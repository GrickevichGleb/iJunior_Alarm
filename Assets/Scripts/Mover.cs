using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _destination;
    [SerializeField] private float _rotationSpeedRad = 5f;
    [SerializeField] private float _moveSpeed = 3f;
    
    private float _reachRadiusSqr = 1f;

    private void Awake()
    {
        _reachRadiusSqr = GetComponent<Collider2D>().bounds.extents.y;
        _reachRadiusSqr *= _reachRadiusSqr;
    }

    private void Update()
    {
        if(_destination == null)
            return;
        
        RotateTowardsDestination();
        MoveTowardsDestination();
    }

    public void SetDestination(Transform newDestination)
    {
        _destination = newDestination;
    }

    public bool HasReachedDestination()
    {
        if (_destination == null)
            return false;

        Vector3 direction = _destination.position - transform.position;
        
        if (direction.sqrMagnitude <= _reachRadiusSqr)
            return true;

        return false;
    }

    private void RotateTowardsDestination()
    {
        Vector3 current = transform.up;
        Vector3 direction = _destination.position - transform.position;
        float rotationStep = _rotationSpeedRad * Time.deltaTime;
        
        transform.up = Vector3.RotateTowards(current, direction, rotationStep, 0f);
    }

    private void MoveTowardsDestination()
    {
        Vector3 direction = _destination.position - transform.position;
        direction.z = 0f;
        
        transform.Translate(direction.normalized * (_moveSpeed * Time.deltaTime), Space.World);
    }
}
