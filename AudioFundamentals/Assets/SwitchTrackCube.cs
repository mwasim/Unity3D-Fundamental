using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchTrackCube : MonoBehaviour
{
    [SerializeField] AudioClip _mainMusicClip;
    [SerializeField] AudioClip _auxMusicClip;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.MainAudioSource.clip = _mainMusicClip;
            AudioManager.Instance.AuxAudioSource.clip = _auxMusicClip;

            AudioManager.Instance.MainAudioSource.Play();
            AudioManager.Instance.AuxAudioSource.Play();
        }
    }
}
