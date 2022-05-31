using UnityEngine;
using Pathfinding;

public class BuzzAI : MonoBehaviour
{
    public Transform player;
    public Transform enemyGFX;
    public Transform firePosition;
    public Animator animator;
    public GameObject projectile;
    private NpcStats npcStats;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float aggroDistance;
    public float attackRange;
    public float attackSpeed;

    private Path path;
    int currentWaypoint = 0;

    private bool isAggresive = false;
    private bool isSafeRange = false;
    private bool isCooldown = false;

    private Seeker seeker;
    private Rigidbody2D rb;

    private void Start()
    {
        npcStats = GetComponent<NpcStats>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, player.position, OnPathComplete);
        }
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update()
    {
        if (npcStats.isDead)
        {
            return;
        }

        SearchAndAttack();
    }

    private void FixedUpdate()
    {
        if (npcStats.isDead)
        {
            return;
        }
        if (path == null) 
        {
            animator.SetFloat("Speed", 0);
            return;
        }
        if (!isAggresive)
        {
            animator.SetFloat("Speed", 0);
            return;
        }
        if (isSafeRange)
        {
            animator.SetFloat("Speed", 0);
            return;
        }

        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        animator.SetFloat("Speed", 1);
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (force.x >= 0.01f)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (force.x <= 0.01f)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    void OnDrawGizmosSelected()
    {
        // Dusmanin menziline ait gizmoslar.
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    void SearchAndAttack()
    {
        if ((Vector2.Distance(transform.position, player.position) < aggroDistance))
        {
            isAggresive = true;
        }
        else
        {
            isAggresive = false;
        }


        if ((Vector2.Distance(transform.position, player.position) < attackRange) && !isCooldown)
        {
            CalculateLookAt();
            animator.Play("Buzz_Attack");
            Invoke("Attack", 0.3f);
            Invoke("ResetCooldown", attackSpeed);
            isCooldown = true;
            isSafeRange = true;
        }

        if ((Vector2.Distance(transform.position, player.position) > attackRange))
        {
            isSafeRange = false;
        }
    }

    void Attack()
    {
        var dir = player.position - transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        GameObject p = Instantiate(projectile, transform.position, Quaternion.AngleAxis(angle, Vector3.forward));
        p.GetComponent<Projectile>().npcStats = GetComponent<NpcStats>();
    }

    void CalculateLookAt()
    {
        if (transform.position.x > player.position.x)
        {
            enemyGFX.localScale = new Vector3(1f, 1f, 1f);
        }
        else if (transform.position.x < player.position.x)
        {
            enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    void ResetCooldown()
    {
        isCooldown = false;
    }
}
