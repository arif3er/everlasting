using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform detectionPoint;
    public float detectionRadius = 0.2f;
    public LayerMask detectionLayer;

    private void Start()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Update()
    {
        
        if (DetectObject())
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            if(InteractInput())
            {
                Debug.Log("Tetiklendi.");
                
            }
        }
        else
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false); 
        }
    }

    bool InteractInput()
    {
       return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        return Physics2D.OverlapCircle(detectionPoint.position,detectionRadius,detectionLayer);
        
    }
    private void OnDrawGizmosSelected()
    {
        
        Gizmos.DrawWireSphere(detectionPoint.position,detectionRadius);
    }
}
