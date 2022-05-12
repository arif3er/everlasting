using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    private bool inDead;
    
    public float maxHealth = 100f;
    public float currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        inDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(float damage)
    {
        currentHealth -= damage;
    }

    private void Death()
    {
        inDead = true;
    }
}
