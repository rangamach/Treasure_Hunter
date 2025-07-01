using System;
using UnityEngine;

public class SoundService
{
    private SoundSO soundSO;
    private AudioSource bgAudioSource;
    private AudioSource sfxAudioSource;
    
    public SoundService(SoundSO SO, AudioSource BG, AudioSource SFX)
    {
        this.soundSO = SO;
        this.bgAudioSource = BG;
        this.sfxAudioSource = SFX;

        PlayBackgroundMusic(SoundType.Background);
    }
    private void PlayBackgroundMusic(SoundType type)
    {
        bgAudioSource.loop = true;
        bgAudioSource.clip = GetSoundClip(type);
        bgAudioSource.Play();
    }
    public void PlaySoundEffect(SoundType type) => sfxAudioSource.PlayOneShot(GetSoundClip(type));
    private AudioClip GetSoundClip(SoundType type)
    {
        Sounds sound = Array.Find(soundSO.sounds,item => item.type == type);
        if(sound.clip == null) return null;
        return sound.clip;
    }
}