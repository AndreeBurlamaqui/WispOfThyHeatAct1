using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class SpitterMarabunta : MonoBehaviour
{

    public GameObject projectile, ass, player;
    public float maxCooldownAttack, circleRadius, arcPower, projectileVelo, projectileMass;
    public LayerMask layer;
    public AnimationStateReference attack;

    private float cooldownAttack;
    private AdvancedPatrol patrol;
    private bool isPlayerFound, isDead;

    private void Start()
    {
        GetComponent<HealthManager>().OnDeathEvent.AddListener(delegate { isDead = true; });

        patrol = GetComponent<AdvancedPatrol>();
    }


    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isPlayerFound)
        {
            if (!patrol.isOnFreeze)
                patrol.isOnFreeze = true;
        }
        else
        {
            if(patrol.isOnFreeze)
                patrol.isOnFreeze = false;
        }

        if (cooldownAttack <= 0)
        {
            if (patrol.shootRaycast)
            {
                RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleRadius, transform.right, 0, layer);

                if (hit.collider != null)
                {

                    Vector3 playerPos = hit.transform.position - transform.position;
                    if (Vector3.Dot(transform.up, playerPos) > 0)
                    {

                        isPlayerFound = true;
                        attack.Play();
                        player = hit.collider.gameObject;

                    }
                }
            }
        }

        if(cooldownAttack > 0) {

            cooldownAttack -= Time.deltaTime;

        }
        
        
    }

    public void Attack()
    {
        if (!isDead)
        {
            GameObject projectilePrefab = Instantiate(projectile, ass.transform.position, Quaternion.LookRotation(ass.transform.up, -transform.forward));
            projectilePrefab.transform.Rotate(Vector3.right, 90f);
            projectilePrefab.GetComponent<ArcShooting>().Shoot(player.GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>().gameObject.transform, projectileVelo);
            cooldownAttack = maxCooldownAttack;

            if (isDead)
            {
                Destroy(projectilePrefab);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

    public void WalkAgain()
    {
        isPlayerFound = false;
    }

}
