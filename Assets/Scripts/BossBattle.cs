using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattle : MonoBehaviour
{
    public AudioClip bossSound;

    private AudioSource source;

    // Start is called before the first frame update
    void Awake()
    {
        
        source = GetComponent<AudioSource>();

        source.clip = bossSound;
    }

    public void StartBattle()
    {
        source.Play();
    }

    public void StopBattle()
    {
        source.Stop();
    }
}
