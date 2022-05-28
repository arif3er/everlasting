using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public Transform firePoint;
    public Transform player;

    void Start()
    {
    }

    void Update()
    {
        UpdateLaser();   
    }

    private void UpdateLaser()
    {
        lineRenderer.SetPosition(0, firePoint.position);

        lineRenderer.SetPosition(1, player.position);

        Vector2 direction = player.position - firePoint.position;
        RaycastHit2D hit = Physics2D.Raycast((Vector2)firePoint.position, direction.normalized, direction.magnitude);

        if (hit)
        {
            if (hit.collider.tag == "Player")
            {

            }
            lineRenderer.SetPosition(1, hit.point);
        }
    }
}
