using System.Collections;
using System.Collections.Generic;
using EZCameraShake;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BeholderAI : MonoBehaviour
{
    private Collider2D hitBox;
    private NpcStats npcStats;

    private Collider2D characterHitBox;

    public Animator animator;
    public Transform firePoint;

    public Transform[] spawnPoints;
    public GameObject redLaser;
    public GameObject blueLaser;
    public GameObject minionBlob;
    public GameObject minionGhast;

    public int attackIndex;

    private bool isDead = false;
    private float health;

    public GameObject projectile;

    public AudioClip[] npcSounds;
    private AudioSource audioSource;

    private void Start()
    {
        characterHitBox = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider2D>();
        npcStats = GetComponent<NpcStats>();
        hitBox = GetComponent<Collider2D>();
        audioSource = GetComponent<AudioSource>();

        hitBox.enabled = false;

        CameraShaker.Instance.ShakeOnce(3f, 5f, .2f, 5f);

        Intro();
        PlayIntroSound();
        PlayBeholderThemeSound();
        Invoke("PlayBeholderThemeSound", 129f);
    }

    private void Update()
    {
        isDead = npcStats.isDead;
        health = npcStats.currentHealth;
    }

    public void Intro()
    {
        Invoke("Stun", 5f);
    }

    private void Stun()
    {
        redLaser.SetActive(false);
        blueLaser.SetActive(false);

        if (isDead) return;

        hitBox.enabled = true;
        Invoke("DecideAttack", 5f);
    }

    private void DecideAttack()
    {
        if (isDead) return;

        hitBox.enabled = false;
        attackIndex = Random.Range(1, 5);

        if (attackIndex == 1)
        {
            if (health <= 350) // K?rm?z? lazer atar, k?rm?z? lazer atar ve blob ça??r?r.
            {
                FireRedLaser();
                SummonBlobs();
            }
            FireRedLaser();
        }
        else if (attackIndex == 2) // Ye?il güdümlü mermi ça??r?r, farkl? yerlerde daha fazla ça??r?r.
        {
            if (health <= 350)
            {
                BoostedSpawnProjectile();
                SpawnProjectile();
            }
            else
            {
                SpawnProjectile();
            }
        }
        else if (attackIndex == 3) // Mavi lazer atar, mavi lazer atar ve ghastify ça??r?r.
        {
            if (health <= 350)
            {
                FireBlueLaser();
                SummonGhastify();
            }
            FireBlueLaser();
        }
        else if (attackIndex == 4) //Blob ça??r?r, daha fazla blob ça??r?r.
        {
            if (health <= 350)
            {
                BoostedSummonBlobs();
            }
            else
            {
                SummonBlobs();
            }
        }

        Invoke("Stun", 5f);
    }

    private void FireRedLaser()
    {
        if (isDead) return;

        CameraShaker.Instance.ShakeOnce(4f, 3f, .1f, 2f);

        PlayLaserSound();
        Invoke("PlayLaserSound", 2.8f);

        redLaser.SetActive(true);

        animator.Play("Beholder_RedLaser");
    }

    private void SpawnProjectile()
    {
        if (isDead) return;

        for (int i = 0; i <3; i++)
        {
            Invoke("PlayProjectileSound", i);
            Invoke("Spawn", i);
        }

        animator.Play("Beholder_GreenLaser");
    }

    private void BoostedSpawnProjectile()
    {
        if (isDead) return;

        for (int i = 0; i < 4; i++)
        {
            Invoke("PlayProjectileSound", i);
        }

        Invoke("BoostedSpawn", 1f);


        animator.Play("Beholder_GreenLaser");
    }

    private void FireBlueLaser()
    {
        if (isDead) return;

        CameraShaker.Instance.ShakeOnce(4f, 3f, .1f, 2f);


        PlayLaserSound();
        Invoke("PlayLaserSound", 2.8f);

        blueLaser.SetActive(true);

        animator.Play("Beholder_BlueLaser");
    }

    private void SummonBlobs()
    {
        if(isDead) return;

        for (int i = 0; i < 3; i++)
        {
            Invoke("PlayBlobSummonSound", i);
            Invoke("SummonB", i);
        }

        animator.Play("Beholder_RedLaser");
    }

    private void BoostedSummonBlobs()
    {
        if (isDead) return;

        for (int i = 0; i < 5; i++)
        {
            Invoke("PlayProjectileSound", i);
            Invoke("SummonB", i);
        }

        animator.Play("Beholder_RedLaser");
    }

    private void SummonGhastify()
    {
        if (isDead) return;

        Invoke("PlayGhastifySummonSound", 1f);
        Invoke("PlayProjectileSound", 1f);
        Invoke("SummonG", 1f);

        animator.Play("Beholder_BlueLaser");
    }

    private void Spawn()
    {
        CameraShaker.Instance.ShakeOnce(2f, 1f, .1f, 2f);
        GameObject prj = Instantiate(projectile, firePoint);
         prj.transform.parent = null;
         prj.transform.position = firePoint.position;
    }

    private void BoostedSpawn()
    {
        CameraShaker.Instance.ShakeOnce(2f, 1f, .1f, 2f);

        for (int i = 1; i < spawnPoints.Length; i++)
        {
            GameObject prj = Instantiate(projectile, firePoint);
            prj.transform.parent = null;
            prj.transform.position = spawnPoints[i].position;
        }
    }

    private void SummonB()
    {
        GameObject blb = Instantiate(minionBlob, spawnPoints[0].transform);
        blb.transform.parent = null;
        blb.transform.position = spawnPoints[0].position;
        CollideFix();
    }

    private void SummonG()
    {
        GameObject gst = Instantiate(minionGhast, spawnPoints[3].transform);
        gst.transform.parent = null;
        gst.transform.position = spawnPoints[3].position;
        CollideFix();

    }

    void PlayProjectileSound()
    {
        audioSource.clip = npcSounds[0];
        audioSource.PlayOneShot(audioSource.clip);
    }

    void PlayLaserSound()
    {
        audioSource.clip = npcSounds[1];
        audioSource.PlayOneShot(audioSource.clip);
    }

    void PlayIntroSound()
    {
        audioSource.clip = npcSounds[2];
        audioSource.PlayOneShot(audioSource.clip);
    }
    void PlayBlobSummonSound()
    {
        audioSource.clip = npcSounds[3];
        audioSource.PlayOneShot(audioSource.clip);
    }

    void PlayGhastifySummonSound()
    {
        audioSource.clip = npcSounds[4];
        audioSource.PlayOneShot(audioSource.clip);
    }

    void PlayBeholderThemeSound()
    {
        audioSource.clip = npcSounds[5];
        audioSource.PlayOneShot(audioSource.clip);
    }


    private void CollideFix()
    {
        GameObject[] arr = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject obj in arr)
        {
            Physics2D.IgnoreCollision(obj.GetComponent<Collider2D>(), characterHitBox);
        }

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length; j++)
            {
                Physics2D.IgnoreCollision(arr[i].GetComponent<Collider2D>(), arr[j].GetComponent<Collider2D>());
            }
        }
    }
}
