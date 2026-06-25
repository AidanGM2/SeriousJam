using UnityEngine;

public class BasicEnemy : MonoBehaviour
{

    [SerializeField]
    private float enemySpeed;


    [SerializeField]
    private float rotationSpeed;



    // public Transform player;
    private Rigidbody2D rb;
    private PlayerAwareness playerAwareness;
    private Vector2 targetDirection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwareness = GetComponent<PlayerAwareness>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        UpdateTargetDirection();
        RotateTowardsTarget();
        SetVelocity();

        /*
        Vector3 direction = player.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        */
    }


    private void UpdateTargetDirection()
    {
        if (playerAwareness.AwareOfPlayer)
        {
            targetDirection = playerAwareness.DirectionToPlayer;
        } else
        {
            targetDirection = Vector2.zero;
        }
    }

    private void RotateTowardsTarget()
    {
        if(targetDirection == Vector2.zero)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        if (targetDirection == Vector2.zero)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            rb.linearVelocity = transform.up * enemySpeed;
        }


    }



}
