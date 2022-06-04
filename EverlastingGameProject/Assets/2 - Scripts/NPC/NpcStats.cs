using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcStats : MonoBehaviour
{
    public float minDamage;
    public float maxDamage;
    public Animator animator;

    public float health;
    public float currentHealth;
    public float deathTimeOut;

    public bool isDead = false;

    public AudioClip[] npcSounds;
    private AudioSource audioSource;

    private void Start()
    {
        currentHealth = health;
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            PlayDieSound();
            animator.Play("Die");
            Invoke("DestroyNPC", deathTimeOut);
        }
    }

    public void TakeDamage(float minDamage, float maxDamage)
    {
        PlayHurtSound();
        animator.Play("Hurt");
        float damage = Random.Range(minDamage, maxDamage);
        currentHealth -= damage;
        
    }

    void PlayHurtSound()
    {
        audioSource.clip = npcSounds[0];
        audioSource.PlayOneShot(audioSource.clip);
    }
    void PlayDieSound()
    {
        audioSource.clip = npcSounds[1];
        audioSource.PlayOneShot(audioSource.clip);
    }

    private void DestroyNPC()
    {
        Destroy(gameObject);
    }
}
