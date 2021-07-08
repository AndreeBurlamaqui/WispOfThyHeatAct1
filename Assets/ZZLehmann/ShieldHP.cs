using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHP : MonoBehaviour
{

    public int hitPoints = 10;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MithraBullet"))
        {
            if (hitPoints > 0)
            {
                hitPoints--;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }


}
