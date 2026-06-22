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


    private Vector3 grapplePoint;
    private DistanceJoint2D joint;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        joint = gameObject.GetComponent<DistanceJoint2D>();
        joint.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //input for movement
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //input for player looking at mouse
        Vector3 mousePos = Input.mousePosition;

        mousePos.z = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);

        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = (mousePos - transform.position);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

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
            }
        }
        //"grabbing" the enemy
        if (grabbedObject != null && grabPoint.GetComponent<Collider2D>().IsTouching(grabbedObject.GetComponent<Collider2D>()))
                {
                    Debug.Log("Object has been grabbed");
                    grabbedObject.GetComponent<Rigidbody2D>().isKinematic = true;
                    grabbedObject.transform.position = grabPoint.transform.position;
                    grabbedObject.transform.SetParent(transform);
                    //this should disable the grapple so the player isn't anchored
                    joint.enabled = false;
                }
        
            //"let go" of grabbed enemy
        if (Input.GetMouseButtonDown(1))
        {
            grabbedObject.GetComponent<Rigidbody2D>().isKinematic = false;
            grabbedObject.transform.SetParent(null);
            grabbedObject = null;
        }
    }

    void FixedUpdate()
    {
        //moving the player
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
