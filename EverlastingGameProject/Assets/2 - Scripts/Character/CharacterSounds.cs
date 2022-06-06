using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSounds : MonoBehaviour
{
    [SerializeField] private Torch torchSound;
    private int footstep = 0;
    
    
    public AudioClip dash;
    public AudioClip die;
    public AudioClip pain;
    public AudioClip swooshAxe;
    public AudioClip[] swooshSounds;
    public AudioClip[] stepSounds;
    public AudioSource audioSource;

    public void PlayStepSound()
    {
        
            if (footstep == 0)
            {
                audioSource.clip = stepSounds[0];
                audioSource.Play();
            
                footstep = 1;
            }
            else if(footstep == 1)
            {
                audioSource.clip = stepSounds[1];
                audioSource.Play();
                footstep = 0;
            }
            
            
        
        
    }
    
    public void PlaySwooshSound0()
    {
        audioSource.clip = swooshSounds[0];
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void PlaySwooshSound1()
    {
        audioSource.clip = swooshSounds[1];
        audioSource.PlayOneShot(audioSource.clip);
    }
    
    public void PlaySwooshAxeSound()
    {
        audioSource.clip = swooshAxe;
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void PlayPainSound()
    {
        audioSource.clip = pain;
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void PlayDeathSound()
    {
        audioSource.clip = die;
        audioSource.PlayOneShot(audioSource.clip);
    }
    public void TorchSound()
    {
        torchSound.TurnOnOfTorch();
    }

    public void PlayDashSound()
    {
        audioSource.clip = dash;
        audioSource.PlayOneShot(audioSource.clip);
    }

    
    
}
