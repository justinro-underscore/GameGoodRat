using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public static SoundController instance = null;
    public AudioSource efxSource;
    public AudioSource secondaryEfxSource;
    public AudioSource tertiaryEfxSource;

    public AudioClip jumpSound;

    private Hashtable soundEffects;

    void Awake ()
    {
        if (instance == null) {
            instance = this;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        InitSounds();
    }

    void InitSounds()
    {
        soundEffects = new Hashtable();
        soundEffects["jumpSound"] = jumpSound;
    }

    public void PlaySingle(string clipName)
    {
        AudioClip clip = (AudioClip)soundEffects[clipName];

        if(!efxSource.isPlaying)
        {
            efxSource.clip = clip;
            efxSource.Play();
        }
        else if (!secondaryEfxSource.isPlaying)
        {
            secondaryEfxSource.clip = clip;
            secondaryEfxSource.Play();
        }
        else
        {
            tertiaryEfxSource.clip = clip;
            tertiaryEfxSource.Play();
        }
    }

    public void StopSounds()
    {
        efxSource.Stop();
        secondaryEfxSource.Stop();
        tertiaryEfxSource.Stop();
    }
}
