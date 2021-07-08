using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class AyusmatFlowerBullet : MonoBehaviour
{
    [SerializeField] [Layer] private int groundLayer;
    [SerializeField] GameObject death;

    private void OnTriggerEnter2D(Collider2D collision)
    {


            if (collision.CompareTag("Player"))
            {
                //collision.GetComponent<Health>().GetDamaged(transform.position.x);
                Death();
            }

            if (collision.gameObject.layer == groundLayer)
            {
                Death();
            }

    }

    public void Death()
    {

        Instantiate(death, transform.position, Quaternion.Euler(-90,0,0));

        Destroy(gameObject);
    }
}
