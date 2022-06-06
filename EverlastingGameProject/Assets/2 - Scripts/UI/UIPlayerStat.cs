using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIPlayerStat : MonoBehaviour
{
    public Image healthBar;
    public Text healthText;
    private float MaxHealth = 200f;

    public CharacterStats characterStats;
   
    void Update()
    {
        healthBar.fillAmount = characterStats.currentHealth / MaxHealth;
        healthText.text = Math.Round (characterStats.currentHealth).ToString();
    }
}
