using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public static FireBall instance { get; private set; }
    private AudioSource _audio;
    private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    public AudioClip fireBall;


    // Start is called before the first frame update
    void Awake()
    {
        _audio = GetComponent<AudioSource>();

        // Asigna los clips de sonido a los identificadores
        soundDictionary["fire"] = fireBall;


        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlaySound(string soundId)
    {
        if (soundDictionary.ContainsKey(soundId))
        {
            _audio.PlayOneShot(soundDictionary[soundId]);
        }
        else
        {
            Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
        }
    }

    public void PlayOnce(string soundId)
    {
        _audio.PlayOneShot(soundDictionary[soundId]);
    }

    public bool IsPlaying(string soundId)
    {
        if (soundDictionary.ContainsKey(soundId))
        {
            return _audio.isPlaying;
        }
        else
        {
            Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
            return false;
        }
    }

    public void StopSound(string soundId)
    {
        if (soundDictionary.ContainsKey(soundId))
        {
            _audio.Stop();
        }
        else
        {
            Debug.LogWarning($"El sonido con el identificador '{soundId}' no existe.");
        }
    }
}
