using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CharacterStats : MonoBehaviour
{
    
    private bool inDead;
    public Animator animator;

    public float characterSpeed = 2f;
    public float maxHealth = 100f;
    public float currentHealth;

    public float minAttackDamge = 30;
    public float maxAttackDamge = 40;

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

    public void SavePlayer()
    {
        SaveSystem.SavePlayer(this);
    }

    public void LoadPlayer()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();

        currentHealth = playerData.health;

        Vector3 position;
        position.x = playerData.position[0];
        position.y = playerData.position[1];
        position.z = playerData.position[2];
        transform.position = position;
    }
}
