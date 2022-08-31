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

    bool soundEffectToggle = true;
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
        ToggleBacksound();
        onePlay.clip = null;

        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        inventory.OnInventoryUpdate += PlayItemTakenSound;   
    }
    
    public void ToggleBacksound()
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
    public void ToggleSFX()
    {
        soundEffectToggle = !soundEffectToggle;
    }
    void PlayItemTakenSound()
    {
        if (soundEffectToggle)
        {
            onePlay.PlayOneShot(itemTakenClip);
        }
    }

}
