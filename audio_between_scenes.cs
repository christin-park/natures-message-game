using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//keeps the audio between all scenes, even through scene switches
public class audio_between_scenes : MonoBehaviour {
    private audio_between_scenes instance;
    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        //DontDestroyOnLoad preserves object during scene switches
        DontDestroyOnLoad(audioSource);
        audioSource.Play();
    }
}