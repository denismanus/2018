using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    private static AudioController instanse;
    private AudioSource mainTheme;
    private AudioClip jump;
    private AudioClip click;
    private AudioClip death;
    private AudioClip teleport;
    private AudioSource[] audios;
    //private AudioSource teleport;
    //private AudioSource death;
    // Use this for initialization
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
        audios = GetComponents<AudioSource>();
        mainTheme = audios[0];
        mainTheme.loop = true;
        mainTheme.clip = Resources.Load<AudioClip>("Sounds/MainTheme");
        jump = Resources.Load<AudioClip>("Sounds/Jump");
        death = Resources.Load<AudioClip>("Sounds/Death");
        teleport = Resources.Load<AudioClip>("Sounds/Teleport");
        click = Resources.Load<AudioClip>("Sounds/Click");
       
    }

    void OnEnable()
    {
        TestScript.OnAction += GetMessage;
        ButtonSound.OnAction += GetMessage;
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (instanse == null)
        {
            instanse = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }

   
    private void GetMessage(string message)
    {
        if (!StaticData.isSoundEnabled)
            return;
        if (message == "Jump")
        {
            JumpSound();
        }
        else if (message == "Death")
        {
            DeathSound();
        }
        else if (message == "Click")
        {
            ClickSound();
        }
        else if (message == "Teleport")
        {
            TeleportSound();
        }
    }
    public void Play()
    {
        if (StaticData.isMusicEnabled)
        {
            mainTheme.UnPause();
        }
    }

    public void Pause()
    {
        mainTheme.Pause();
    }

    private void JumpSound()
    {
        audios[1].PlayOneShot(jump, 1f);
    }
     private void DeathSound()
    {
        audios[1].PlayOneShot(death, 1f);
    }
     private void TeleportSound()
    {
        audios[1].PlayOneShot(teleport, 3f);
    }

    private void ClickSound()
    {
        audios[1].PlayOneShot(click, 1f);
    }
    public void PlayFromBegin()
    {
        if (StaticData.isMusicEnabled)
        {
            mainTheme.Stop();
            mainTheme.Play();
        }
    }
}
