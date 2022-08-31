using LSWTest.Inventory;
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

    Inventory inventory;

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

        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        inventory.OnInventoryUpdate += PlayItemTakenSound;

        
    }
    void PlayBacksound()
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
    void PlayItemTakenSound()
    {
        onePlay.PlayOneShot(itemTakenClip);
    }
}
