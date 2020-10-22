using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _mainAudioSource;
    [SerializeField] private AudioSource _auxAudioSource;
    [SerializeField] private AudioSource _ambientAudioSource;
    [SerializeField] private AudioSource _eventAudioSource;

    [SerializeField] private AudioMixerSnapshot _eventSnapshot;
    [SerializeField] private AudioMixerSnapshot _idleSnapshot;

    public AudioSource MainAudioSource => _mainAudioSource;
    public AudioSource AuxAudioSource => _auxAudioSource;
    public AudioSource AmbienceAudioSource => _ambientAudioSource;
    public AudioSource EventAudioSource => _eventAudioSource;

    private void Awake()
    {
        Instance = this;
    }


    public IEnumerator PlayEventMusic()
    {
        _eventSnapshot.TransitionTo(0.25f);
        yield return new WaitForSeconds(0.3f);

        _eventAudioSource.Play();

        while (_eventAudioSource.isPlaying) yield return null;

        _idleSnapshot.TransitionTo(025f);

        yield break;
    }
}
