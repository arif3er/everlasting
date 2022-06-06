using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare : MonoBehaviour
{
    public GameObject beforeScare;
    public GameObject afterScare;
    private Collider2D _collider;

    public AudioClip[] npcSounds;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        _collider = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayScareSound();

            _collider.gameObject.SetActive(false);
            beforeScare.SetActive(false);
            afterScare.SetActive(true);

            Invoke("SetBack", 2f);
        }
    }

    private void SetBack()
    {
        beforeScare.SetActive(true);
        afterScare.SetActive(false);
        Destroy(gameObject);
    }

    void PlayScareSound()
    {
        Debug.Log("SES");
        audioSource.clip = npcSounds[0];
        audioSource.PlayOneShot(audioSource.clip);
    }
}
