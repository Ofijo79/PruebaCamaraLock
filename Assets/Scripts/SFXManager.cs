using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public AudioClip Dash;

    public AudioClip Fire;

    private AudioSource source;

    public AudioClip swordMove;

    public AudioClip Destruction;

    public AudioClip getHit;

    public AudioClip run;

    public AudioClip Jump;
    
    public AudioClip swordHit;

    public AudioClip Death;


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        source = GetComponentInChildren<AudioSource>();
    }

    public void Dashing()
    {
        source.PlayOneShot(Dash);
    }
    
    public void FireBall()
    {
        source.PlayOneShot(Fire);
    }
    public void SwordSound()
    {
        source.PlayOneShot(swordMove);
    }
    public void DestructionSound()
    {
        source.PlayOneShot(Destruction);
    }
    public void GetHit()
    {
        source.PlayOneShot(getHit);
    }
    public void Run()
    {
        source.PlayOneShot(run);
    }
    public void JumpSound()
    {
        source.PlayOneShot(Jump);
    }
    public void SwordHit()
    {
        source.PlayOneShot(swordHit);
    }
    public void DeathSound()
    {
        source.PlayOneShot(Death);
    }
}
