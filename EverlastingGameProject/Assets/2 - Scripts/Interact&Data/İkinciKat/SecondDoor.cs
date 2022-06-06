using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondDoor : MonoBehaviour
{
    public GameObject button;
    public Transform teleportPoint;

    private GameObject player;

    private bool pickUp;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        player.transform.position = teleportPoint.position;

        Destroy(gameObject);
    }
}
