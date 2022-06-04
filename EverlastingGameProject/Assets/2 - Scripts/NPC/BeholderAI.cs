using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeholderAI : MonoBehaviour
{
    private Collider2D hitBox;

    public Animator animator;

    public Transform firePoint;

    public GameObject redLaser;
    public GameObject blueLaser;

    public float blueLaserDamage;
    public float redLaserDamage;
    public float greenLaserDamage;

    public int attackIndex;

    public bool isDead = false;

    public GameObject projectile;

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
        redLaser.SetActive(false);

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
            SpawnProjectile();
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

        redLaser.SetActive(true);
        animator.Play("Beholder_RedLaser");
    }

    private void SpawnProjectile()
    {
        if (isDead) return;

        for (int i = 0; i <3; i++)
        {
            Invoke("Spawn", i);
        }

        animator.Play("Beholder_GreenLaser");
    }

    private void FireBlueLaser()
    {
        if (isDead) return;

        redLaser.SetActive(true);

        animator.Play("Beholder_BlueLaser");
    }

    private void Spawn()
    {
            GameObject prj = Instantiate(projectile, firePoint);
            prj.transform.parent = null;
            prj.transform.position = firePoint.position;
    }
}
