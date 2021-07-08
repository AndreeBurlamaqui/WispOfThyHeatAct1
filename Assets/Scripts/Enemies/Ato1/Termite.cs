using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Termite : MonoBehaviour
{

    public float speedCrazy, distanceRayCrazy;
    public bool queenAlive = true;
    public int minQueenLife;
    public GameObject windFX;
    public Collider2D queenCollider;

    [Separator("Animations")]
    [SerializeField] private AnimationStateReference queenDeath;
    [SerializeField] private AnimationStateReference hurtQueen;

    private GameObject queen;
    private FlyWithinArea fly;
    private BoucingProjectile crazy;
    private HealthManager hm;

    void Start()
    {
        queen = transform.Find("MarabuntaQueen").gameObject;
        fly = GetComponent<FlyWithinArea>();
        crazy = GetComponent<BoucingProjectile>();
        hm = GetComponent<HealthManager>();
        windFX.SetActive(false);
    }


    private void Update()
    {


            if (queenAlive)
            {

                fly.Fly();

            }
            else
            {
                
                crazy.ReflectiveShooting(speedCrazy, distanceRayCrazy);

                if (!windFX.activeSelf)
                {
                    windFX.SetActive(true);
                    GetComponent<Animator>().applyRootMotion = true;
                }

            }
        
    }

    public void HurtQueen()
    {

        if (queenAlive)
        {

            transform.rotation = Quaternion.identity;

            hurtQueen.Play();

            if(hm.currentLife <= minQueenLife)
            {
                queenCollider.enabled = false;
                queenAlive = false;

                if (queen != null)
                {
                    if (queen.activeSelf)
                    {
                        Destroy(queen);
                    }
                }
            }
        }

    }

}
