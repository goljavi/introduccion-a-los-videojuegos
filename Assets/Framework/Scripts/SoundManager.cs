using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

    public AudioSource music;
    public AudioSource ui;
    public AudioSource sfx;

    private float _masterVolume = 0.5f;
    private float _sfxVolume = 0.5f;
    private float _musicVolume = 0.5f;
    private float _uiVolume = 0.5f;

    public enum SoundType
    {
        Master,
        SFX,
        Music,
        UI
    }

    /// <summary>
    /// Reproduce un sonido
    /// </summary>
    /// <param name="soundType">Tipo de sonido</param>
    /// <param name="audioClip">Sonido</param>
    /// <param name="audioSource">Si el tipo es SFX, necesita un AudioSource</param>
    public void ReproduceSound(SoundType soundType, AudioClip audioClip, AudioSource audioSource = null)
    {
        float volume;
        switch(soundType)
        {
            case SoundType.SFX:
                volume = _masterVolume * _sfxVolume * 3;
                sfx.PlayOneShot(audioClip, volume);
                break;

            case SoundType.Music:
                music.Stop();
                volume = _masterVolume * _musicVolume;
                music.clip = audioClip;
                music.Play();
                break;

            case SoundType.UI:
                volume = _masterVolume * _uiVolume;
                ui.PlayOneShot(audioClip, volume);
                break;
        }
    }


    /// <summary>
    /// Deja de reproducir la música
    /// </summary>
    public void StopMusic()
    {
        music.Stop();
    }

    #region SetVolumes

    public void SetMasterVolume(float i)
    {
        if (i < 0)
            i = 0;
        else if (i > 1)
            i = 1;
        _masterVolume = i;
        SetVolumes();
    }

    public float GetMasterVolume()
    {
        return _masterVolume;
    }

    public void SetSFXVolume(float i)
    {
        if (i < 0)
            i = 0;
        else if (i > 1)
            i = 1;
        _sfxVolume = i;
        SetVolumes();
    }

    public float GetSFXVolume()
    {
        return _sfxVolume;
    }

    public void SetMusicVolume(float i)
    {
        if (i < 0)
            i = 0;
        else if (i > 1)
            i = 1;
        _musicVolume = i;
        SetVolumes();
    }

    public float GetMusicVolume()
    {
        return _musicVolume;
    }

    public void SetUIVolume(float i)
    {
        if (i < 0)
            i = 0;
        else if (i > 1)
            i = 1;
        _uiVolume = i;
        SetVolumes();
    }

    public float GetUIVolume()
    {
        return _uiVolume;
    }

    public void SetVolumes()
    {
        music.volume = _masterVolume * _musicVolume;
        ui.volume = _masterVolume * _uiVolume;
    }

    #endregion
}