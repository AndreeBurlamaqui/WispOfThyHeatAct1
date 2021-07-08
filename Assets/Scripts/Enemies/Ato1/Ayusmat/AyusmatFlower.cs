using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AyusmatFlower : MonoBehaviour
{

    public float cooldown, maxCooldown;
    [SerializeField] private float bulletSpeed;

    public GameObject bullet, death;
    public Transform shootingPoint;
    private bool isDead;

    void Start()
    {
        GetComponent<HealthManager>().OnDeathEvent.AddListener(delegate { isDead = true; });
        FindObjectOfType<BossBattle_Ayusmat>().AddFlower(gameObject);
        cooldown = maxCooldown;

    }

    // Update is called once per frame
    void Update()
    {

        if (cooldown <= 0) {

            if(!isDead)
                Shoot();
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

    }

    public void Shoot()
    {
        GameObject seedPF1 = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        seedPF1.transform.rotation = Quaternion.Euler(0, 0, 45);
        seedPF1.GetComponent<Rigidbody2D>().AddForce(seedPF1.transform.up * bulletSpeed, ForceMode2D.Force);

        GameObject seedPF2 = Instantiate(bullet, shootingPoint.position, Quaternion.identity);
        seedPF2.transform.rotation = Quaternion.Euler(0, 0, -45);
        seedPF2.GetComponent<Rigidbody2D>().AddForce(seedPF2.transform.up * bulletSpeed, ForceMode2D.Force);

        if (isDead)
        {
            Destroy(seedPF1);
            Destroy(seedPF2);
        }

        cooldown = maxCooldown;
    }
}
