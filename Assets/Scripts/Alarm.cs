using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Alarm : MonoBehaviour
{
    [SerializeField] private float _volumeChangeRate = 0.2f;
    
    private float _currentVolume;
    private float _targetVolume;
    
    private float _radiusSqr;

    private AudioSource _audioSource;
    private Thief _thief;

    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start()
    {
        _radiusSqr = gameObject.GetComponent<CircleCollider2D>().radius;
        _radiusSqr *= _radiusSqr;
    }

    private void Update()
    {
        if (_thief == null)
            return;

        float distanceSqr = (_thief.transform.position - gameObject.transform.position).sqrMagnitude;
        _targetVolume = 1 - (distanceSqr / _radiusSqr);

        _currentVolume = Mathf.MoveTowards(_currentVolume, _targetVolume, _volumeChangeRate * Time.deltaTime);
        _audioSource.volume = _currentVolume;
    }
    
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out _thief))
        {
            _audioSource.Play();
            _audioSource.loop = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out _thief))
        {
            _audioSource.Stop();
            _thief = null;
        }
    }
}
