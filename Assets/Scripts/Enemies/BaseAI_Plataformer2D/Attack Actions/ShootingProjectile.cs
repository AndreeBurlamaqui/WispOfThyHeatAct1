using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingProjectile : MonoBehaviour
{

    public GameObject shootPrefab;

    public bool isMultipleShoot,isMultiplePoint, isArch, isSnipping, isBullseye, isBadAim;
    public int ShootQtd;
    public float archForce;

    //Needs a child named shootingPoint
    //Needs a child named directionPoint or a child named directions with all directions
    private Transform shootingPoint, directionPoint;

    void Start()
    {
        shootingPoint = transform.Find("shootingPoint");

        if (!isMultiplePoint)
        {
            if (transform.Find("directionPoint"))
            {
                directionPoint = transform.Find("directionPoint");
            }
        }
    }

    public void Shoot()
    {
        if (isMultipleShoot)
        {
            if (isArch)
            {

            }
            
            //Instant MultipleShooting
            if(isSnipping)
            {

            }

            //Target player
            if (isBullseye)
            {

            }

            //Define direction
            if (isBadAim)
            {

                if (isMultiplePoint)
                {
                    for (int x = 0; x < ShootQtd; x++)
                    {
                        Transform dir = transform.Find("directions");

                        GameObject shootGO = Instantiate(shootPrefab, shootingPoint.transform.position, Quaternion.identity);
                        Vector3 direction = dir.transform.GetChild(x).transform.position - shootingPoint.position;
                        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                        shootGO.transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

                        if (isArch)
                        {
                            shootGO.GetComponent<Rigidbody2D>().AddForce(direction.normalized * archForce);
                        }
                        else
                        {
                            
                        }

                    }
                }
                else
                {

                    for (int x = 0; x < ShootQtd; x++)
                    {
                        GameObject shootGO = Instantiate(shootPrefab, shootingPoint.transform.position, Quaternion.identity);

                        shootGO.transform.forward = directionPoint.position - shootingPoint.position;
                    }
                }


            }
        } else
        {
            if (isArch)
            {

            }
            else
            {

            }
        }
    }
}
