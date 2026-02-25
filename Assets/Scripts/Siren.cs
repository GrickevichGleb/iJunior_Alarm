using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Siren : MonoBehaviour
{
    [SerializeField] private float _volumeChangeRate = 0.2f;
    
    private float _currentVolume;
    private float _targetVolume;
    
    private AudioSource _audioSource;
    private Transform _intruder;
    private float _radiusSqr;
    private bool _isAdjustingAlarm = false;
    
    private void Awake()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void ActivateSiren(Transform intruder, float alarmRadiusSqr)
    {
        _intruder = intruder;
        _radiusSqr = alarmRadiusSqr;
        _isAdjustingAlarm = true;

        StartCoroutine(AdjustAlarmVolumeCoroutine());
    }

    public void DeactivateSiren()
    {
        _isAdjustingAlarm = false;
    }
    
    private IEnumerator AdjustAlarmVolumeCoroutine()
    {
        _audioSource.Play();
        _audioSource.loop = true;
        
        while (_isAdjustingAlarm)
        {
            float distanceSqr = (_intruder.position - gameObject.transform.position).sqrMagnitude;
            _targetVolume = 1 - (distanceSqr / _radiusSqr);

            _currentVolume = Mathf.MoveTowards(_currentVolume, _targetVolume, _volumeChangeRate * Time.deltaTime);
            _audioSource.volume = _currentVolume;

            yield return null;
        }
        
        _audioSource.Stop();
        _intruder = null;
    }
}
