using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _splashSound;
    [SerializeField] private AudioMixerSnapshot _idleSnapshot;
    [SerializeField] private AudioMixerSnapshot _auxInSnapshot;
    [SerializeField] private AudioMixerSnapshot _ambIdleSnapshot;
    [SerializeField] private AudioMixerSnapshot _ambInSnapshot;
    [SerializeField] private LayerMask _enemyMask;

    private AudioSource _audioSource;
    private bool _isEnemyNear;
   
    // Update is called once per frame
    void Update()
    {
        var hits = Physics.SphereCastAll(transform.position, 5f, transform.forward, 0f, _enemyMask);

        if(hits.Length > 0) //enemies nearby
        {
            if (!_isEnemyNear)
            {
                _auxInSnapshot.TransitionTo(0.5f);

                _isEnemyNear = true;
            }
        }
        else
        {
            if (_isEnemyNear)
            {
                _idleSnapshot.TransitionTo(0.5f);
                _isEnemyNear = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            _audioSource.PlayOneShot(_splashSound);
        }

        if (other.CompareTag("EnemyZone"))
        {
            _auxInSnapshot.TransitionTo(0.5f);
        }

        if (other.CompareTag("Ambience"))
        {
            _ambInSnapshot.TransitionTo(0.5f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            _audioSource.PlayOneShot(_splashSound);
        }

        if (other.CompareTag("EnemyZone"))
        {
            _idleSnapshot.TransitionTo(0.5f);
        }

        if (other.CompareTag("Ambience"))
        {
            _ambIdleSnapshot.TransitionTo(0.5f);
        }
    }
}
