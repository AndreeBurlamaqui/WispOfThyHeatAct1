using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MithraFireRange : MonoBehaviour
{
    public float circleRadius, smoothTime;
    public Vector3 velocity;
    public GameObject lockCrosshair, lockCHGO, onRangeEnemy;
    public ParticleSystem crosshairPS;

    public bool onLock = false;

    [SerializeField] private MithraController mMov;
    private void OnEnable()
    {
        mMov = transform.parent.GetComponent<MithraController>();
        //mMov.autoAim = true;
    }

    private void OnDisable()
    {
        mMov = transform.parent.GetComponent<MithraController>();
        //mMov.autoAim = false;
    }

    private void Update()
    {
        if (onLock)
        {
            if (crosshairPS.isPlaying)
            {
                lockCHGO.transform.position = Vector3.SmoothDamp(lockCHGO.transform.position, onRangeEnemy.transform.position, ref velocity, smoothTime);
            }
            else
            {
                /*if (mMov.canShoot)
                {
                    mMov.onRange = true;
                    crosshairPS.Play();
                }*/
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (mMov.canShoot)
        {
            if (collision.CompareTag("Enemy"))
            {
                if (!onLock)
                {
                    lockCHGO = Instantiate(lockCrosshair, collision.gameObject.transform.position, Quaternion.identity);
                    crosshairPS = lockCHGO.transform.GetChild(0).GetComponent<ParticleSystem>();

                    onLock = true;
                    mMov.onRange = true;
                    onRangeEnemy = collision.gameObject;
                    mMov.enemyOnRange = onRangeEnemy;
                }

                
            }
        }
        /*else if(lockCHGO != null)
        {
            crosshairPS.Stop();
        }*/
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if(collision.gameObject == onRangeEnemy)
            {
                onLock = false;
                mMov.onRange = false;
                onRangeEnemy = null;
                mMov.enemyOnRange = onRangeEnemy;
                Destroy(lockCHGO);
            }
        }
    }

}
