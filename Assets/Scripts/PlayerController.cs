using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //movement variables
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 movement;

    //grab variables
    [SerializeField] private float grappleLength;
    [SerializeField] private LayerMask grappleLayer;
    [SerializeField] private GameObject grabPoint;
    private GameObject grabbedObject;

    //spinning variables
    Vector3 AngleVelocity;
    bool isSpinning;
    private float revSpeed = 50f;


    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        AngleVelocity = new Vector3(0, 1000, 0);
        isSpinning = false;
    }

    // Update is called once per frame
    void Update()
    {
        //input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //input for player looking at mouse
        if(isSpinning == false){
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Vector2 direction = (mousePos - transform.position);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
        }

        //grab mechanics
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(
                origin: Camera.main.ScreenToWorldPoint(Input.mousePosition),
                direction: Vector2.zero,
                distance: Mathf.Infinity,
                layerMask: grappleLayer
            );

            if(hit.collider != null)
            {
                //pulls the player to the enemy
                grapplePoint = hit.point;
                grapplePoint.z = 0;
                joint.connectedAnchor = grapplePoint;
                joint.enabled = true;
                joint.distance = grappleLength;
                grabbedObject = hit.collider.gameObject;
                grabbedObject.layer = 0;
                grabbedObject.GetComponent<EnemyBehavior>().isGrabbed = true;
            }
        }
        //"grabbing" the enemy
        if (grabbedObject != null && grabPoint.GetComponent<Collider2D>().IsTouching(grabbedObject.GetComponent<Collider2D>()))
        {
                    grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabbedObject.transform.position = grabPoint.transform.position;
                    grabbedObject.transform.SetParent(transform);
                    //this should disable the grapple so the player isn't anchored
                    joint.enabled = false;
                    isSpinning = true;
        }
        
            //"let go" of grabbed enemy
        if (Input.GetMouseButtonDown(1))
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);
            grabbedObject.GetComponent<EnemyBehavior>().isGrabbed = false;
            grabbedObject.layer = 3;
            grabbedObject = null;
            isSpinning = false;
            revSpeed = 50f;
        }
    }

    void FixedUpdate()
    {
        //moving the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        //the spinning
        if(isSpinning == true){
            //Quaternion deltaRotation = Quaternion.Euler(m_EulerAngleVelocity * Time.fixedDeltaTime);
            rb.MoveRotation(rb.rotation + revSpeed * Time.fixedDeltaTime);
            if(revSpeed <= 750f)
            {
                revSpeed += 5f;
            }
        }
    }
    
    
}
