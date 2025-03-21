using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TerrainScannerDEMO
{
    public class SensorObject : MonoBehaviour
    {
        [SerializeField] private SensorDetector _detector;

        private AudioSource _audioTrigger;
        [SerializeField] AudioClip _sensorExit;

        [SerializeField] private Material _detectedMaterial;
        public Material _cachedMaterial;

        private MeshRenderer _meshRenderer;

        private bool _detected;

        private float _timeToReset;
        private float _timerToReset;

        [SerializeField]private  MissionWaypoint missionWaypoint;

        private void Start()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _cachedMaterial = _meshRenderer.sharedMaterial;
            _audioTrigger = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_detected)
            {
                if (_timerToReset > _timeToReset)
                {
                    if (_detector.SensorOn) { return; }
                    _detected = false;
                    _timerToReset = 0f;
                    _meshRenderer.sharedMaterial = _cachedMaterial;
                }

                _timerToReset += Time.deltaTime;
            }

            if (_detector.SensorOn)
            {
                if (Vector3.Distance(transform.position, _detector.Origin) < (_detector.Radius - 1f))
                {
                    if (_detected)
                    {
                        return;
                    }
                    Detected();
                }
            }
        }

        private void Detected()
        { 
            _detected = true;
            _audioTrigger.PlayOneShot(_sensorExit);
            missionWaypoint.enabled = true;
            _meshRenderer.sharedMaterial = _detectedMaterial;
        }
    }
}
