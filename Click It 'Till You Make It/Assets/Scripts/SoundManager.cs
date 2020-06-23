using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip music;
    AudioSource audioSource;

    public List<AudioClip> sFXs;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = music;
        audioSource.Play();
    }

    public void PlaySound(int sFXNumber)
    {
        AudioSource.PlayClipAtPoint(sFXs[sFXNumber], Vector3.zero);
    }
}