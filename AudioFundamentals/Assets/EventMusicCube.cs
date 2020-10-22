using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMusicCube : MonoBehaviour
{
    [SerializeField] AudioClip _eventMusicClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.EventAudioSource.clip = _eventMusicClip;
            AudioManager.Instance.StartCoroutine(nameof(AudioManager.Instance.PlayEventMusic));
        }
    }
}
