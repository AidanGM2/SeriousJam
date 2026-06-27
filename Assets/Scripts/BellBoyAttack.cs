using UnityEngine;

public class BellBoyAttack : MonoBehaviour
{

    public bool AwareOfPlayer { get; private set; }
    [SerializeField]
    private float playerAwarenessDistance;

    private Transform player;

    public GameObject slash;

    private Animator anim;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask playerLayer;

    public int attackDamage = 40;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindAnyObjectByType<PlayerController>().transform;
        anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
        */
        
        if(Vector2.Distance(transform.position, player.transform.position) <= playerAwarenessDistance)
        {
            Attack();
        }
        





        /*
        Vector2 enemyToPlayerVector = player.position - transform.position;
        if (enemyToPlayerVector.magnitude <= playerAwarenessDistance)
        {
            AwareOfPlayer = true;
            Attack();
        }
        else
        {
            AwareOfPlayer = false;
            anim.SetBool("isAttacking", false);
        }
        */
    }


    void Attack()
    {
        //Debug.Log("Im gonna wack ya!");
        //anim.SetBool("isAttacking", true);
        anim.SetTrigger("Attack");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, playerLayer);

        foreach(Collider2D player in hitPlayer)
        {
            //Debug.Log("I hit the player");
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
        }
        
    }


    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
