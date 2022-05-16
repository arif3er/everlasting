using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
   [SerializeField] private float characterSpeed = 2f;
   public Animator animator;

   private void Update()
   {
      Vector3 movement = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0.0f);
      
      animator.SetFloat("Horizontal",movement.x);         //Yatay düzlemde hareket girdisi.
      animator.SetFloat("Vertical",movement.y);           //Dikey düzlemde hareket girdisi.
      animator.SetFloat("Magnitude",movement.magnitude);  //Hareket ediyor kontrolü.
      
      transform.position = transform.position + movement * Time.deltaTime * characterSpeed;  //Hareket metodu.
   }

   private void Attack()
   {
      
   }
}
