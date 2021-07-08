using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
public class AyusmatBullet : MonoBehaviour
{

    [SerializeField] [Layer] private int groundLayer;
    [SerializeField] private GameObject archFlower, death;
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            Instantiate(archFlower, new Vector3(transform.position.x, transform.position.y + 0.3f, 0), Quaternion.identity);
            Death();
        }

        if (collision.CompareTag("Player"))
        {
            Death();
        }
    }

    private void Death()
    {

        //Instantiate(death, transform.position, Quaternion.Euler(-90, 0, 0));

        Destroy(gameObject);
    }
}
