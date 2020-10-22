using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource _mainAudioSource;
    [SerializeField] private AudioSource _auxAudioSource;
    [SerializeField] private AudioSource _ambientAudioSource;

    public AudioSource MainAudioSource => _mainAudioSource;
    public AudioSource AuxAudioSource => _auxAudioSource;
    public AudioSource AmbienceAudioSource => _ambientAudioSource;

    private void Awake()
    {
        Instance = this;
    }
}
