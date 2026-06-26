using Pathfinding;
using UnityEngine;


public class StarEnemy : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    [SerializeField]
    private float rotationSpeed;

    Path path;

    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    private Vector2 targetDirection;
    public Vector2 DirectionToPlayer { get; private set; }

    private void Awake()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
        
    }

    private void UpdatePath()
    {
        if (seeker.IsDone())
        
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        
        
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }


    void FixedUpdate()
    {
        RotateTowardsTarget();
        UpdateTargetDirection();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 enemyToPlayerVector = target.position - transform.position;
        DirectionToPlayer = enemyToPlayerVector.normalized;


        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;


        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }

    private void RotateTowardsTarget()
    {
        
        if (targetDirection == Vector2.zero)
        {
            return;
        }
        
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }


    private void UpdateTargetDirection()
    {
        targetDirection = DirectionToPlayer;
    }



}
