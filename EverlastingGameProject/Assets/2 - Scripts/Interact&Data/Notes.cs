using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Notes : MonoBehaviour
{
    public GameObject button;
    public Canvas canvas;
    public Text _text;

    private GameObject player;

    private bool pickUp;
    private bool isOpen = false;

    public string lore;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        button.SetActive(false);
    }

    private void Update()
    {
        if (!isOpen && pickUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUp();
        }else if (isOpen && Input.GetKeyDown(KeyCode.E))
        {
            canvas.gameObject.SetActive(false);
            isOpen = false;
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
        canvas.gameObject.SetActive(true);
        _text.text = lore;
        isOpen = true;
    }
}
