using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip _splashSound;
    [SerializeField] private AudioMixerSnapshot _idleSnapshot;
    [SerializeField] private AudioMixerSnapshot _auxInSnapshot;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

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
    }
}
