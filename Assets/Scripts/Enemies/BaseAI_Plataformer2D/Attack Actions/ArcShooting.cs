using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(Rigidbody2D))]
public class ArcShooting : MonoBehaviour
{
    public float rotateSpeed;
    [SerializeField] [Layer] private int groundWallLayer;
    [SerializeField] private float destroyTimer = 3f;
    private float speed;
    private Quaternion targetPos;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Shoot(Transform target, float newSpeed)
    {
        if (rb == null)
        {
            if (TryGetComponent(out Rigidbody2D tryRb))
            {
                rb = tryRb;
            }
        }

        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        targetPos = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

        speed = newSpeed;
    }

    void FixedUpdate()
    {
        rb.velocity = transform.up * (speed * 2) * Time.deltaTime;
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetPos, rotateSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetPos, rotateSpeed * Time.deltaTime);
        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundWallLayer || collision.CompareTag("Player"))
        {
            DestroyBullet();
        }
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);

    }
}
