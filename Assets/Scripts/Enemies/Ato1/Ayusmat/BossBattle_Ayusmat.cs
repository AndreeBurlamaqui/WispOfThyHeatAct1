using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class BossBattle_Ayusmat : MonoBehaviour
{
    [Separator("Variables", true)]
    [SerializeField] private float normalSpeed = 0.5f;
    [SerializeField] private float slowSpeed = 0.2f, highSpeed = 0.8f, smoothFollowCart = 0.3f, minChanceToSeed = 75f, bulletSpeed, playerMinDistance, tooCloseDistance, playerWaitDistance;
    [SerializeField] private float[] bulletAngle = new float[3];
    [SerializeField] private bool fightStarted, waitinPlayer;


    [Separator("Game Objects & Transforms", true)]
    [SerializeField] private Transform shootingPlace;
    [SerializeField] private GameObject[] seedTypes = new GameObject[2];
    private Transform player;
    private List<GameObject> flowers = new List<GameObject>();

    [Separator("Other Scripts", true)]
    [SerializeField] private Cinemachine.CinemachineDollyCart dollyCart;

    [Separator("Animations", true)]
    [SerializeField] private AnimationStateReference hitGround;
    [SerializeField] private AnimationStateReference seedAttack;


    private void Start()
    {
        player = PlayerManager.Instance.GetPlayerGO.transform;
    }

    private void FixedUpdate()
    {
        if (fightStarted)
        {
            float dist = Vector3.Distance(transform.position, player.position);

            if (!waitinPlayer)
            {
                if (dist <= playerMinDistance)
                {
                    if (dist <= tooCloseDistance)
                    {
                        dollyCart.m_Speed = highSpeed;
                    }
                    else
                    {
                        dollyCart.m_Speed = normalSpeed;
                    }

                    transform.position = Vector3.Lerp(transform.position, dollyCart.gameObject.transform.position, smoothFollowCart * Time.deltaTime);
                }
                else
                {
                    dollyCart.m_Speed = 0;
                }
            }
            else
            {
                dollyCart.m_Speed = 0;

                if (dist <= playerWaitDistance)
                {
                    if (waitinPlayer)
                        waitinPlayer = false;

                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent != null)
        {
            if (collision.transform.parent.gameObject.name == "WaitPlayer")
            {
                MovementState(false);
            }

            if (collision.transform.parent.gameObject.name == "BallSeedAttack")
            {
                SeedAttack();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<DialogueHolder>() != null)
            MovementState(false);
    }

    private void SeedAttack()
    {
        seedAttack.Play();
        GameObject seedPF =  null;

        for (int x = 0; x < 3; x++)
        {
            if (Random.Range(0, 100) >= minChanceToSeed)
            {

                seedPF = Instantiate(seedTypes[0], shootingPlace.position, Quaternion.identity);

            }
            else
            {

                seedPF = Instantiate(seedTypes[1], shootingPlace.position, Quaternion.identity);

            }

            seedPF.transform.rotation = Quaternion.Euler(0, 0, bulletAngle[x]);
            seedPF.GetComponent<Rigidbody2D>().AddForce(seedPF.transform.up * bulletSpeed, ForceMode2D.Force);
            
        }
 
    }

    public void MovementState(bool state)
    {
        waitinPlayer = !state;
    }

    public void StartBossBattle()
    {
        fightStarted = true;
        CameraManager.Instance.ChangeToGroupCam(transform);
    }

    public void HitGround()
    {
        hitGround.Play();
        StartCoroutine(CameraManager.Instance.ShakeRumble(CameraManager.ShakeRumbleIntensity.High, 3f, false));
    }

    public void EndBossBattle()
    {
        CameraManager.Instance.StopGroupCam();

        foreach(GameObject f in flowers)
        {
            DestroyImmediate(f);
        }
    }

    public void AddFlower(GameObject flowerToAdd)
    {
        flowers.Add(flowerToAdd);
    }

    private void OnDisable()
    {
        foreach (GameObject f in flowers)
        {
            DestroyImmediate(f);
        }
    }
}
