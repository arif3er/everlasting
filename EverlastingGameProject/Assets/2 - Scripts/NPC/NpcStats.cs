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

    private void Start()
    {
        currentHealth = health;
    }
   
    public void TakeDamage(float minDamage, float maxDamage)
    {
        animator.Play("Hurt");
        float damage = Random.Range(minDamage, maxDamage);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.Play("Die");
            Invoke("DestroyNPC", deathTimeOut);
        }
    }

    private void DestroyNPC()
    {
        Destroy(gameObject);
    }
}
