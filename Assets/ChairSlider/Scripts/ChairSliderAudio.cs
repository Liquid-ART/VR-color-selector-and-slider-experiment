using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChairSliderAudio : MonoBehaviour
{
    [SerializeField]
    private Sound[] sounds;
    private SoundPlayer soundPlayer = new SoundPlayer();

    private AudioSource audioSource;
    private GameObject audioTransitionsObj;
    private Timer timer = new Timer();


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioTransitionsObj = transform.GetChild(0).gameObject;
        ChairSliderEvents.OnPlayHoverSound += PlayHoverSound;
        ChairSliderEvents.OnPlaPressSound += PlayPressSound;
        ChairSliderEvents.OnSlide += PlaySlideSound;
        ChairSliderEvents.OnChangeControl += PlaySelectorSounds;
    }

    private void Update()
    {
        if(timer.OnceTimerIsComplete())
            soundPlayer.Play(2, sounds, audioTransitionsObj);
    }

    private void PlayHoverSound()
    {
        if (audioSource.isPlaying == false)
            soundPlayer.Play(0, sounds, gameObject);
    }

    private void PlayPressSound()
    {
            soundPlayer.Play(1, sounds, gameObject);
    }


    private void PlaySlideSound(Vector2 _notImplemented, float _notImplemented2)
    {
        timer.SetTimer(sounds[1].clip.length/2);
    }

    private void PlaySelectorSounds(int _notImplemented, float _notImplemented2, bool _expand)
    {
        if(_expand)
            soundPlayer.Play(3, sounds, audioTransitionsObj);
        else
            soundPlayer.Play(4, sounds, audioTransitionsObj);
    }

}
