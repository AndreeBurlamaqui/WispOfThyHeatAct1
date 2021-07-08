using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class BulletMithra : MonoBehaviour
{

    private Rigidbody2D rb;
    [SerializeField] [Layer] private int groundWallLayer; 
    public float speed;
    public float destroyTimer = 2f;
    [SerializeField] private GameObject deathFX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();                
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = transform.up * speed * Time.deltaTime * 2;

        destroyTimer -= Time.deltaTime;

        if(destroyTimer <= 0){
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if(!other.CompareTag("Player")){
            if(other.CompareTag("Enemy")){
                //other.getcomponent.gethit
                if(other.GetComponent<HealthManager>() != null)
                    DestroyBullet();
            }
            if(other.gameObject.layer == groundWallLayer){
                DestroyBullet();
            }

        }
    }

    private void DestroyBullet(){
        //instancia animação de morte
        Instantiate(deathFX, transform.position, Quaternion.identity);
        deathFX.SetActive(true);


        Destroy(gameObject);
    }

}
