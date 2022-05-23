using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    private NpcStats npcStats;
    public CharacterStats characterStats;

    private void OnTriggerEnter2D(Collider2D other)
    {
        npcStats = other.GetComponent<NpcStats>();

        if (other.tag == "Enemy")
        {
            npcStats.TakeDamage(characterStats.minAttackDamge, characterStats.maxAttackDamge);
        }
    }
}
