using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    private bool soundOn;
    private float soundLevel = 1;
    private AudioSource effectsAudio;
    private AudioSource backGroundAudio;

    public bool SoundOn
    {
        get
        {
            return soundOn;
        }

        set
        {
            soundOn = value;
        }
    }

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void SetUp(AudioClip backGroundMusic)
    {
        LoadSound();
        effectsAudio = gameObject.AddComponent<AudioSource>();
        effectsAudio.playOnAwake = false;
        backGroundAudio = gameObject.AddComponent<AudioSource>();
        backGroundAudio.clip = backGroundMusic;

        if (soundLevel > 0.5f)
        {
            SoundOn = true;
            backGroundAudio.enabled = true;
            backGroundAudio.Play();
        }
        else
        {
            SoundOn = false;
            backGroundAudio.enabled = false;
        }
        effectsAudio.enabled = SoundOn;
    }

    public void SoundOnOff(bool on)
    {
        if (on)
        {
            soundLevel = 1;
        }
        else
        {
            soundLevel = 0;
        }
        SaveSound();
        SoundOn = on;
        effectsAudio.enabled = on;
        backGroundAudio.enabled = on;
    }

    public void PlaySound(AudioClip audioClip)
    {
        if (SoundOn)
        {
            effectsAudio.PlayOneShot(audioClip);
        }
    }

    private void LoadSound()
    {
        if (PlayerPrefs.HasKey("HensSound"))
        {
            soundLevel = PlayerPrefs.GetFloat("HensSound");
        }
        else
        {
            soundLevel = 1;
        }
    }

    private void SaveSound()
    {
        PlayerPrefs.SetFloat("HensSound", soundLevel);
    }
}
