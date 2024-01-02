using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField] private AudioClip musicClips;
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource[] soundEffects;

    public bool isMusicPlay = true;
    public bool isEffectPlay = true;

    public IconOnOffManager musicIcon;
    public IconOnOffManager fxIcon;
    private void Awake()
    {
        instance = this;
    }
    
    public void PlaySoundEffect(int whichSound)
    {
        if (isEffectPlay && whichSound < soundEffects.Length)
        {
            //soundEffects[whichSound].volume = PlayerPrefs.GetFloat("FXVolume");
            soundEffects[whichSound].Stop();
            soundEffects[whichSound].Play();
        }
    }

    public void BackgroundMusicPlay(AudioClip musicClip)
    {
        if (!musicClip || !musicSource || !isMusicPlay)
        {
            return;
        }

        musicSource.volume = PlayerPrefs.GetFloat("musicVolume");
        musicSource.clip = musicClip;
        musicSource.Play();
    }

    void MusicUpdateFNC()
    {
        if (musicSource.isPlaying != isMusicPlay)
        {
            if (isMusicPlay)
            {
                BackgroundMusicPlay(musicClips);
            }
            else
            {
                musicSource.Stop();
            }
        }
    }

    public void PlayButtonSoundEffect()
    {
        PlaySoundEffect(2);
    }

    public void MusicOnOffFNC()
    {
        isMusicPlay = !isMusicPlay;
        MusicUpdateFNC();
        //musicIcon.IconOnOffFNC(isMusicPlay);
    }

    public void FXOnOffFNC()
    {
        isEffectPlay = !isEffectPlay;
        
        //fxIcon.IconOnOffFNC(isEffectPlay);
    }
}
