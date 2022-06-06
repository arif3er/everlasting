using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public AudioClip torchSound;
    private bool isActive = false;
    public AudioSource audioSource;
    public void TurnOnOfTorch()
    {
        if (isActive)
        {
            audioSource.clip = null;
            audioSource.Stop();
            isActive = false;
        }
        else if(!isActive)
        {
            audioSource.clip = torchSound;
            isActive = true;
            InvokeRepeating("PlayTorch",0f,15f);    
        }
        
    }

    private void PlayTorch()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
    
}
