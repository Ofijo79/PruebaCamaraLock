using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip menuSound;

    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        source = GetComponentInChildren<AudioSource>();

        source.clip = menuSound;
        source.Play();
    }

    public void StopBGM()
    {
        source.Stop();
    }

    public void ResumeBGM()
    {
        source.Play();
    }
}
