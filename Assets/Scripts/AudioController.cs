using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
    private AudioSource source;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("Sounds/MainTheme");
    }

    public void Play()
    {
        if (StaticData.isSounded)
        {
            source.UnPause();
        }
    }

    public void Pause()
    {
        source.Pause();
    }

    public void PlayFromBegin()
    {
        if (StaticData.isSounded)
        {
            source.Play();
        }
    }
}
