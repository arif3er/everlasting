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
        PlayerPrefs.SetInt(gameObject.name + "_dead", 0);
        LoadEnemy();
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
        SaveEnemy();
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

    private void SaveEnemy()
    {
        PlayerPrefs.SetInt(gameObject.name + "_dead", currentHealth <= 0 ? 1 : 0);
    }

    private void LoadEnemy()
    {
        int dead = PlayerPrefs.GetInt(gameObject.name + "_dead");
        Debug.Log(health);
        if (dead == 1)
        {
            Destroy(gameObject);
        }
    }
}
