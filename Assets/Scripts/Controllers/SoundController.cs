using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour {
    public static SoundController instance = null;
    public AudioSource efxSource;
    public AudioSource secondaryEfxSource;
    public AudioSource tertiaryEfxSource;
    public AudioSource musicSource;

    public AudioClip jumpSound;
    public AudioClip death;
    public AudioClip gameStart;
    public AudioClip itemCorrect;
    public AudioClip itemWrong;
    public AudioClip grab;
    public AudioClip boom;
    public AudioClip hurt;

    public List<AudioClip> scared;
    public List<AudioClip> thanks;

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
        soundEffects["death"] = death;
        soundEffects["gameStart"] = gameStart;
        soundEffects["itemCorrect"] = itemCorrect;
        soundEffects["itemWrong"] = itemWrong;
        soundEffects["grab"] = grab;
        soundEffects["boom"] = boom;
        soundEffects["hurt"] = hurt;
    }

    public void PlaySingle(string clipName)
    {
        AudioClip clip;
        if (clipName == "scared") {
            int index = (int)Mathf.Floor(UnityEngine.Random.Range(0, scared.Count));
            clip = scared[index];
        }
        else if (clipName == "thanks") {
            int index = (int)Mathf.Floor(UnityEngine.Random.Range(0, thanks.Count));
            clip = thanks[index];
        }
        else {
            clip = (AudioClip)soundEffects[clipName];
        }

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

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void StartMusic()
    {
        musicSource.Play();
    }
}
