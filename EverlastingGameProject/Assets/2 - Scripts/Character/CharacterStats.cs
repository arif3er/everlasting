using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    
    private bool inDead;
    public Animator animator;
    public float maxHealth = 100f;
    public float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        inDead = false;
    }

    void Update()
    {
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
        
    }

    private void Death()
    {
        animator.SetBool("Death",true);
        inDead = true;
        Debug.Log("Öldün Kardeş");
    }
}
