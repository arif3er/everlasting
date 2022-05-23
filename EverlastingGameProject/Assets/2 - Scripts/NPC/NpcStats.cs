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

    public bool isDead = false;

    private void Start()
    {
        currentHealth = health;
    }

    private void Update()
    {
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.Play("Die");
            Invoke("DestroyNPC", 1);
        }
    }

    public void TakeDamage(float minDamage, float maxDamage)
    {
        animator.Play("Hurt");
        float damage = Random.Range(minDamage, maxDamage);
        currentHealth -= damage;
    }



    private void DestroyNPC()
    {
        Destroy(gameObject);
    }
}
