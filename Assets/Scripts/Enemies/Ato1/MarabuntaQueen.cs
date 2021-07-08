using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class MarabuntaQueen : MonoBehaviour
{

    public float maxCooldownAttack, circleRadius, arcPower, projectileVelo;
    public LayerMask layer;
    public GameObject projectile;

    [SerializeField] private AnimationStateReference attackAnimation;
    private GameObject player, ass;
    private float cooldownAttack;

    void Start()
    {
        ass = transform.Find("Ass").gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (cooldownAttack <= 0)
        {
            RaycastHit2D hit = Physics2D.CircleCast(transform.position, circleRadius, transform.right, 0, layer);

            if (hit.collider != null)
            {
                attackAnimation.Play();

                if(player == null)
                    player = hit.collider.gameObject;

            }
        }

        if (cooldownAttack > 0)
        {
            cooldownAttack -= Time.deltaTime;
        }

    }


    public void Attack()
    {
        bool isDead = !GetComponentInParent<Termite>().queenAlive;

        if (!isDead)
        {
            GameObject projectilePrefab = Instantiate(projectile, ass.transform.position, Quaternion.LookRotation(ass.transform.up, -transform.forward));
            projectilePrefab.transform.Rotate(Vector3.right, 90f);
            projectilePrefab.GetComponent<ArcShooting>().Shoot(player.GetComponentInChildren<UnityEngine.Experimental.Rendering.Universal.Light2D>().gameObject.transform, projectileVelo);
            cooldownAttack = maxCooldownAttack;

            if (isDead)
                Destroy(projectilePrefab);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, circleRadius);
    }

}
