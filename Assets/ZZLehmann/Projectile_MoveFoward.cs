using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile_MoveFoward : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 800f;
    private float destroyTimer = 3f;
    void Start()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * speed * Time.deltaTime * 2;

        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

            if (other.CompareTag("Player"))
            {
                //other.getcomponent.gethit

                DestroyBullet();
            }
            if (other.gameObject.layer == 8)
            {
                DestroyBullet();
            }

        
    }

    private void DestroyBullet()
    {
        //instancia animação de morte

        Destroy(gameObject);
    }
}
