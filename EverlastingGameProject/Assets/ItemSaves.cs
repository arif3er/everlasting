using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSaves : MonoBehaviour
{
    public int hasKey = 0;

    public void setKey()
    {
        hasKey = 1;
        SaveItem();
    }

    private void Start()
    {
       // PlayerPrefs.SetInt(gameObject.name + "_key", 0);
        LoadItem();
    }

    private void SaveItem()
    {
        PlayerPrefs.SetInt(gameObject.name + "_key", hasKey);
    }

    private void LoadItem()
    {
        int i = PlayerPrefs.GetInt(gameObject.name + "_key");

        gameObject.GetComponent<ItemSaves>().hasKey = i;
    }
}
