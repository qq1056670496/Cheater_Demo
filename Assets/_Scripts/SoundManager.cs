using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager _instance;
    private AudioSource bgmAudio;
    private AudioSource playerEffectAudio;
    private AudioSource otherEffectAudio;
    private string path = "Sounds/";

    private List<AudioSource> _audios=new List<AudioSource>();
    private float _savedVolume = 0.5f;
    private bool _canPlay = true;
    private void Awake()
    {
        _instance = this;
        PlayAudio("Bgm2",0.2f).loop=true;
    }

    public AudioSource PlayAudio(string name,float volume = 1)
    {
        if (_canPlay == false) return null;
        AudioSource tempAudio = null;
        foreach (var audio in _audios)
        {
            if (audio.isPlaying == false)
            {
                tempAudio = audio;
                break;
            }
        }
        if (tempAudio == null)
        {
            tempAudio = gameObject.AddComponent<AudioSource>();
        }
        _audios.Add(tempAudio);
        tempAudio.clip = Resources.Load<AudioClip>(path + name) as AudioClip;
        tempAudio.volume = volume*_savedVolume;
        tempAudio.Play();
        StartCoroutine(SetCanPlayDelay());
        return tempAudio;
    }
    IEnumerator SetCanPlayDelay()
    {
        _canPlay = false;
        yield return new WaitForSeconds(0.05f);
        _canPlay = true;

    }


    public void OnButtonClick()
    {
        PlayAudio("Click");
    }
    public void ChangeSoundVolume(float value)
    {
        _savedVolume = value;
        foreach (var audio in _audios)
        {
            if (audio.volume <= 0.2f)
            {
                audio.volume = 0.2f * value;
            }
            else
            {
                audio.volume = value;
            }
        }
        //bgmAudio.volume = value * 0.2f;
        //playerEffectAudio.volume = value;
        //otherEffectAudio.volume = value;
    }
    public void StopAllSounds()
    {
        foreach (var audio in _audios)
        {
            if(audio.isPlaying)
                audio.Stop();
        }
    }
}
