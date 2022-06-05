using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public float health;
    public float[] position;

    public PlayerData(CharacterStats stats)
    {
        health = stats.currentHealth;

        position = new float[3];
        position[0] = stats.transform.position.x;
        position[1] = stats.transform.position.y;
        position[2] = stats.transform.position.z;
    }
}
