using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientSoundController : MonoBehaviour
{
    private bool playerHere = false;

    public AudioClip[] npcSounds;
    private AudioSource audioSource;

    public float sound0ReplayTime;
    public float sound1ReplayTime;
    public float sound2ReplayTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        InvokeRepeating("PlaySound0", 0f, sound0ReplayTime);
        InvokeRepeating("PlaySound1", 0f, sound1ReplayTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHere = true;
        }
    }

    void PlaySound0()
    {
        if (!playerHere) return;

        audioSource.clip = npcSounds[0];
        audioSource.PlayOneShot(audioSource.clip);
    }

    void PlaySound1()
    {
        if (!playerHere) return;

        audioSource.clip = npcSounds[1];
        audioSource.PlayOneShot(audioSource.clip);
    }

    void PlaySound2()
    {
        if (!playerHere) return;

        audioSource.clip = npcSounds[2];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
