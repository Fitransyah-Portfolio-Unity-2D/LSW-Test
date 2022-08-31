using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSetting : MonoBehaviour
{
    static AudioSetting instance = null;

    public AudioClip backsoundClip;
    public AudioClip itemTakenClip;

    public AudioSource onePlay;
    public AudioSource backsound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        backsound.clip = backsoundClip;
        PlayBacksound();
        onePlay.clip = null;
    }
    public  void PlayBacksound()
    {
        if (!backsound.isPlaying)
        {
            backsound.Play();
        }
        else
        {
            backsound.Stop();
        }
    }
    public void ItemTakenSound()
    {
        onePlay.PlayOneShot(itemTakenClip);
    }
}
