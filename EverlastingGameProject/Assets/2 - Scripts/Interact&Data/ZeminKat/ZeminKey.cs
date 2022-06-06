using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZeminKey : MonoBehaviour
{
    public ZeminKatSaves saves;
    public GameObject button;

    private bool pickUp;

    private void Start()
    {
        button.SetActive(false);
    }

    private void Update()
    {
        if (pickUp && Input.GetKey(KeyCode.E))
        {
            PickUp();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            button.SetActive(true);
            pickUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            button.SetActive(false);
            pickUp = false;
        }
    }

    void PickUp()
    {
        saves.key = true;
        Destroy(gameObject);
    }
}
