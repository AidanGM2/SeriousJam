using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public bool isGrabbed;
    [SerializeField] float radius;
    [SerializeField] LayerMask enemies;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed == true)
        {
            Collider2D hitEnemy = Physics2D.OverlapCircle(this.transform.position, radius, enemies);
            if(hitEnemy != null)
            {
                hitEnemy.GetComponent<EnemyBehavior>().die();
            }
        }
    }

    void die()
    {
        //maybe make this fancier later lol
        Destroy(this.gameObject);
    }
}
