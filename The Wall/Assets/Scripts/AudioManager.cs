using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource _aud;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        _aud = GetComponent<AudioSource>();
    }

    public void PlayMe(AudioClip clip)
    {
        float randPitch = Random.Range(0.8f, 1.1f);
        float randVOl = Random.Range(0.5f, 1f);

        _aud.volume = randVOl;
        _aud.pitch = randPitch;
        _aud.PlayOneShot(clip);



    }


}