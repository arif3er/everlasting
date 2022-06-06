using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorSound : MonoBehaviour
{
    private bool inPlaying = false;
    public AudioClip horrorSound;
    private AudioSource audioSource;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && inPlaying == false)
        {
            inPlaying = true;
            audioSource.clip = horrorSound;
            audioSource.Play();
            Invoke("Delete", 4f);
        }
    }
    private void Delete()
    {
        Destroy(gameObject);
    }
}
