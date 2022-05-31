using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderAI : MonoBehaviour
{
    private Collider2D hitBox;

    public Animator animator;

    public Transform firePoint;

    public GameObject redLaser;
    public GameObject greenLaser;
    public GameObject blueLaser;

    public float blueLaserDamage;
    public float redLaserDamage;
    public float greenLaserDamage;

    public int attackIndex;

    public bool isDead = false;

    private void Start()
    {
        hitBox = GetComponent<Collider2D>();
        Intro();
    }

    public void Intro()
    {
        Invoke("Stun", 5f);
    }

    private void Stun()
    {
        if (isDead) return;

        hitBox.enabled = true;
        Invoke("DecideAttack", 5f);
    }

    private void DecideAttack()
    {
        if (isDead) return;

        hitBox.enabled = false;
        attackIndex = Random.Range(1, 4);

        if (attackIndex == 1)
        {
            FireRedLaser();
        }
        else if (attackIndex == 2)
        {
            FireGreenLaser();
        }
        else if (attackIndex == 3)
        {
            FireBlueLaser();
        }

        Invoke("Stun", 5f);
    }

    private void FireRedLaser()
    {
        if (isDead) return;

        redLaser.GetComponent<LineRenderer>().SetColors(Color.red, Color.red);
        redLaser.SetActive(true);
        animator.Play("Beholder_RedLaser");
    }

    private void FireGreenLaser()
    {
        if (isDead) return;

        redLaser.GetComponent<LineRenderer>().SetColors(Color.green, Color.green);
        greenLaser.SetActive(true);
        animator.Play("Beholder_GreenLaser");
    }

    private void FireBlueLaser()
    {
        if (isDead) return;

        redLaser.GetComponent<LineRenderer>().SetColors(Color.blue, Color.blue);
        blueLaser.SetActive(true);
        animator.Play("Beholder_BlueLaser");
    }
}
