using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scare : MonoBehaviour
{
    public GameObject beforeScare;
    public GameObject afterScare;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _collider.gameObject.SetActive(false);
            beforeScare.SetActive(false);
            afterScare.SetActive(true);

            Invoke("SetBack", 2f);
        }
    }

    private void SetBack()
    {
        beforeScare.SetActive(true);
        afterScare.SetActive(false);
        Destroy(gameObject);
    }
}
