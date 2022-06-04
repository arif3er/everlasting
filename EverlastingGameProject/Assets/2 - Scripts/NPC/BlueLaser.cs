using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLaser : MonoBehaviour
{
    private CharacterStats characterStats;
    public LineRenderer lineRenderer;

    public Transform firePoint;
    public Transform player;
    public GameObject startVFX;
    public GameObject endVFX;

    public float blueLaserMinDamage;
    public float blueLaserMaxDamage;

    private bool isHitting = false;

    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void Start()
    {
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        StartCoroutine("DealDamage");

        FillLists();
    }

    void Update()
    {
        UpdateLaser();
    }

    private void UpdateLaser()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            particles[i].Play();
        }

        lineRenderer.SetPosition(0, (Vector2)firePoint.position);
        startVFX.transform.position = (Vector2)firePoint.position;
        

        lineRenderer.SetPosition(1, player.position);

        firePoint.right = player.position - firePoint.position;

        Vector2 direction = player.position - firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction.normalized, direction.magnitude);

        if (hit)
        {
            isHitting = hit.collider.tag == "Player";

            lineRenderer.SetPosition(1, hit.point);
        }

        endVFX.transform.position = lineRenderer.GetPosition(1);
    }

    IEnumerator DealDamage()
    {
        if (isHitting)
        {
            characterStats.TakeDamage(blueLaserMinDamage, blueLaserMaxDamage);
        }

        yield return new WaitForSeconds(0.5f);

        StartCoroutine("DealDamage");
    }

    void FillLists()
    {
        for (int i = 0; i < startVFX.transform.childCount; i++)
        {
            var pos = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (pos != null)
            {
                particles.Add(pos);
            }
        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var pos = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (pos != null)
            {
                particles.Add(pos);
            }
        }
    }
}
