using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    private CharacterStats characterStats;

    public float healAmount;

    void Start()
    {
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }
    
    private void Use()
    {
        if (characterStats.currentHealth != 100)
        {
            characterStats.currentHealth += healAmount;
            if (characterStats.currentHealth > 100)
            {
                characterStats.currentHealth = 100;
            }
        }
    }

}
