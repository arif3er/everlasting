using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    private bool isActive = false;
    public AudioSource audioSource;
    public void TurnOnOfTorch()
    {
        if (isActive)
        {
            audioSource.Stop();
            isActive = false;
        }
        else
        {
            isActive = true;
            InvokeRepeating("PlayTorch",0f,15f);    
        }
        
    }

    private void PlayTorch()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
    
}
