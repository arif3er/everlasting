using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneMusic : MonoBehaviour
{
    public AudioClip horrorSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = horrorSound;
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        audioSource.Play();
        yield return new WaitForSeconds(27f);
        PlayMusic();
    }
}   
