using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueLaser : MonoBehaviour
{
    private CharacterStats characterStats;
    public LineRenderer lineRenderer;

    public Transform firePoint;
    public Transform player;

    public float blueLaserMinDamage;
    public float blueLaserMaxDamage;

    private bool isHitting = false;

    private void Start()
    {
        characterStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        StartCoroutine("DealDamage");
    }

    void Update()
    {
        UpdateLaser();
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);

        lineRenderer.SetPosition(1, player.position);

        firePoint.right = player.position - firePoint.position;

        Vector2 direction = player.position - firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction.normalized, direction.magnitude);

        if (hit)
        {
            isHitting = hit.collider.tag == "Player";

            lineRenderer.SetPosition(1, hit.point);
        }
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
}
