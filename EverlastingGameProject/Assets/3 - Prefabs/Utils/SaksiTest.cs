using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaksiTest : MonoBehaviour
{
    public ItemSaves itemSaves;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            itemSaves.setKey();
        }
    }
}
