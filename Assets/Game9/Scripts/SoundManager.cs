using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance;
    public AudioClip SquishSound;

    [Tooltip("How many Audio Sources in the pool")]
    public int SoundBufferSize = 5;

    // Make some audio sources to use as buffers
    private AudioSource[] audios;
    private int _currentAudioSource = 0;

    void Awake()
    {
        Instance = this;

        //Create a pool of audiosource and initialize
        if (audios == null || audios.Length.Equals(0))
        {
            audios = new AudioSource[SoundBufferSize];
            for (int i = 0; i < audios.Length; i++)
            {
                audios[i] = gameObject.AddComponent<AudioSource>();
                audios[i].priority = 255;
                audios[i].volume = 1;
            }
        }
    }

    public void PlaySquishSound()
    {
        PlaySound(SquishSound);
    }

    public void PlaySound(AudioClip sound, float delay = 0, float volume = 1.0f, float pitch = 1.0f)
    {
        (audios[_currentAudioSource]).clip = sound;
        audios[_currentAudioSource].volume = volume;
        audios[_currentAudioSource].pitch = pitch;
        (audios[_currentAudioSource]).PlayDelayed(delay);

        //Increase buffer index
        _currentAudioSource++;
        if (_currentAudioSource > audios.Length - 1)
            _currentAudioSource = 0;
       
    }
}
