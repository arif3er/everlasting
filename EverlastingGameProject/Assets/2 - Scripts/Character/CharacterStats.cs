using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    
    private bool inDead;
    public Animator animator;

    private float activeWeapon = 0;
    
    public float characterSpeed = 2f;
    public float maxHealth = 100f;
    public float currentHealth;

    public float minAxeDamage = 30,maxAxeDamage = 50;
    public float minSwordDamage = 15 , maxSwordDamage = 30;
    
    public float minAttackDamage;
    public float maxAttackDamage;

    void Start()
    {
        currentHealth = maxHealth;
        inDead = false;
    }

    void Update()
    {
        if (activeWeapon == 0)
        {
            minAttackDamage = minSwordDamage;
            maxAttackDamage = maxSwordDamage;
        }
        else if (activeWeapon == 1)
        {
            minAttackDamage = minAxeDamage;
            maxAttackDamage = maxAxeDamage;
        }
        
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float minDamage, float maxDamage)
    {
        animator.Play("Hurt");
        float damage = Random.Range(minDamage, maxDamage);
        currentHealth -= damage;
    }

    private void Death()
    {
        animator.SetBool("Death",true);
        inDead = true;
    }

    public void ChangeWeapon()
    {
        if (activeWeapon == 0)
        {
            activeWeapon = 1;
        }
        else if(activeWeapon == 1)
        {
            activeWeapon = 0;
        }
    }
}
