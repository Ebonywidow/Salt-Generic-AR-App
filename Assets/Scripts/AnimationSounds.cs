using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(AudioSource))]
public class AnimationSounds : MonoBehaviour {

    public AudioClip skateAudioClip;
    public AudioClip idleAudioClip;
    public AudioClip slideAudioClip;
    public AudioClip feedAudioClip;

    private AudioSource audioToPlay;

    public void PlayClip(string smName) {
        switch ( smName ) {
            case "Skate":
                audioToPlay = GameObject.Find("Penguin").GetComponent<AudioSource>();
                audioToPlay.PlayOneShot(skateAudioClip);
                break;
            case "Idle":
                audioToPlay = GameObject.Find("Penguin").GetComponent<AudioSource>();
                audioToPlay.PlayOneShot(idleAudioClip);
                break;
            case "Feed":
                audioToPlay = GameObject.Find("Penguin").GetComponent<AudioSource>();
                audioToPlay.PlayOneShot(feedAudioClip);
                break;
            case "Slide":
                audioToPlay = GameObject.Find("Penguin").GetComponent<AudioSource>();
                audioToPlay.PlayOneShot(slideAudioClip);
                break;
        }
    }

}
