using Pathfinding;
using UnityEngine;

public class StarEnemy : MonoBehaviour
{

    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;


    Path path;

    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;


    private void Awake()
    {
        target = FindAnyObjectByType<PlayerController>().transform;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

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


    // Update is called once per frame
    void Update()
    {
        
    }
}
