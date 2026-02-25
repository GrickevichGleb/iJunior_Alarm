using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private Siren _siren;

    private float _radiusSqr;
    private Thief _thief;
    
    private void Start()
    {
        _radiusSqr = gameObject.GetComponent<CircleCollider2D>().radius;
        _radiusSqr *= _radiusSqr;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out _thief))
            _siren.ActivateSiren(_thief.transform, _radiusSqr);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out _thief))
            _siren.DeactivateSiren();
    }
}
