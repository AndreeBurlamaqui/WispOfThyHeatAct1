using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
public class Patrol : MonoBehaviour, IGrounded
{

    public float speed, distance, freezingDuration;
    public Transform groundCheck;
    public LayerMask groundLayer;

    public bool isOnFreeze = false;
    private Vector2 direction;
    private Animator anim;
    private void Awake()
    {
        HealthManager hpManager = GetComponent<HealthManager>();
        
        hpManager.OnHitEvent.AddListener(delegate { StartCoroutine(StopWalking()); });
        hpManager.OnDeathEvent.AddListener(delegate { isOnFreeze = true; });
        freezingDuration = hpManager.enemyKnockbackDuration;

        anim = GetComponent<Animator>();
    }
    IEnumerator StopWalking()
    {
        isOnFreeze = true;
        anim.enabled = false;
        yield return new WaitForSeconds(freezingDuration);
        isOnFreeze = false;
        anim.enabled = true;
    }
    public void Walk()
    {

        if (!isOnFreeze)
        {
            

            transform.Translate(transform.right * transform.localScale.x * speed * Time.deltaTime);

            RaycastHit2D wallInfo = Physics2D.Raycast(transform.position, transform.right * transform.localScale.x, distance, groundLayer);

            if (wallInfo.collider || !CheckGround())
            {
                Flip();
            }


        }
    }

    private void Flip()
    {
	
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }

    public bool CheckGround()
    {

        RaycastHit2D groundInfo = Physics2D.Raycast(groundCheck.position, -transform.up, distance, groundLayer);
        Debug.DrawRay(groundCheck.position, -transform.up * distance);

        if (groundInfo.collider)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
