using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Experimental.Rendering.Universal;

public class BeeBoss : MonoBehaviour
{

    public GameObject BOSS;

    [Header("Spawns")]
    public GameObject rightWallSpawn;
    public GameObject leftWallSpawn, roofSpawn, groundSpawn;


    [Header("Attacks")]
    public GameObject bulletSpear;
    public GameObject[] shieldStages = new GameObject[3];
    public GameObject shieldThrow;
    public bool isAttacking = false;
    [SerializeField] [ReadOnly] private float currentCDAttack;
    private GameObject player;
    private enum BossStages { Beginning, Middle, End, Critical };[SerializeField] private BossStages bossStage = 0;

    [Foldout("Bullet Spear Attacks", true)]
    public float aiminDuration, shootALotDuration;

    [Foldout("Shield Attacks", true)]
    public float minWaitShield, maxWaitShield, shieldInAirDuration;
    public Collider2D shieldCollider;
    public float shieldSpeed;

    [Foldout("Wall Attacks", true)]
    public float minCDAttack, maxCDAttack;
    public float minCriticalCDAttack, maxCriticalCDAttack;
    public float wallAttackDuration;
    public AnimationCurve wallAttackAC;


    

    private void Start()
    {
        shieldStages[0].transform.parent.gameObject.SetActive(false);
        player = GameObject.FindWithTag("Player").GetComponentInChildren<Light2D>().gameObject;

        shieldCollider = shieldThrow.GetComponent<Collider2D>();
        shieldCollider.enabled = false;
        Hide();
    }

    private void Update()
    {
        if (!isAttacking)
        {
            if (currentCDAttack > 0)
            {
                currentCDAttack -= Time.deltaTime;
            }
            else
            {
                switch (Random.Range(1, 5))
                {
                    case 1:
                        WallAttack();
                        break;
                    case 4:
                        WallAttack();
                        break;
                    case 2:
                        BulletSpearAttack();
                        break;
                    case 3:
                        ShieldOn();
                        break;
                    default:
                        WallAttack();
                        break;
                }
            }
        }
    }

    public void Hide()
    {
        GameObject closestGO = null;
        float smallestDist = 0;

        for (int x = 0; x < 3; x++)
        {
            if (x == 0) {
                smallestDist = Vector2.Distance(BOSS.transform.position, rightWallSpawn.transform.position);
                closestGO = rightWallSpawn;
            }

            if(x == 1)
            {
                float leftWallDist;
                leftWallDist = Vector2.Distance(BOSS.transform.position, leftWallSpawn.transform.position);

                if(leftWallDist < smallestDist)
                {
                    smallestDist = leftWallDist;
                    closestGO = leftWallSpawn;
                }
            }

            if (x == 2)
            {
                float roofDist;
                roofDist = Vector2.Distance(BOSS.transform.position, roofSpawn.transform.position);

                if (roofDist < smallestDist)
                {
                    smallestDist = roofDist;
                    closestGO = roofSpawn;
                }
            }
        }

        BOSS.transform.position = closestGO.transform.position;

        if (bossStage != BossStages.Critical)
        {
            currentCDAttack = Random.Range(minCDAttack, maxCDAttack);
        }
        else
        {
            currentCDAttack = Random.Range(minCriticalCDAttack, maxCriticalCDAttack);
        }

        isAttacking = false;
        BOSS.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

    }

    public void WallAttack()
    {
        isAttacking = true;
        int randomWall=0;
        if(bossStage != BossStages.Critical)
        {
            randomWall = Random.Range(1, 4);
        }
        else
        {
            randomWall = Random.Range(1, 5);
        }
        Debug.Log(randomWall);
        switch (randomWall)
        {
            case 1:
                //left to right
                BOSS.transform.position = leftWallSpawn.transform.position;
                StartCoroutine(WallAttackMovement(BOSS.transform.position, rightWallSpawn.transform.position));
                BOSS.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));

                break;
            case 2:
                //right to left
                BOSS.transform.position = rightWallSpawn.transform.position;
                StartCoroutine(WallAttackMovement(BOSS.transform.position, leftWallSpawn.transform.position));
                BOSS.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));

                break;
            case 3:
                //above player to ground
                BOSS.transform.position =  new Vector2(player.transform.position.x, roofSpawn.transform.position.y);
                StartCoroutine(WallAttackMovement(BOSS.transform.position, new Vector2(BOSS.transform.position.x, groundSpawn.transform.position.y)));
                BOSS.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                break;
            case 4:
                //below player to roof
                BOSS.transform.position = new Vector2(player.transform.position.x, groundSpawn.transform.position.y);
                StartCoroutine(WallAttackMovement(BOSS.transform.position, new Vector2(BOSS.transform.position.x, roofSpawn.transform.position.y)));
                BOSS.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                break;
            default:
                BOSS.transform.position = leftWallSpawn.transform.position;
                StartCoroutine(WallAttackMovement(BOSS.transform.position, rightWallSpawn.transform.position));
                BOSS.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90f));

                break;
        }

    }

    IEnumerator WallAttackMovement(Vector2 origin, Vector2 target)
    {
        float journey = 0f;
        while (journey <= wallAttackDuration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / wallAttackDuration);

            float curvePercent = wallAttackAC.Evaluate(percent);
            BOSS.transform.position = Vector3.LerpUnclamped(origin, target, curvePercent);

            yield return null;
        }

        Hide();
    }


    public void BulletSpearAttack()
    {
        isAttacking = true;

            StartCoroutine(MoveToSpear(BOSS.transform.position, new Vector2(Random.Range(-22, 22), roofSpawn.transform.position.y - 5f), 1f));

    }

    

    IEnumerator MoveToSpear(Vector2 origin, Vector2 target, float duration)
    {
        float journey = 0f;
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);

            BOSS.transform.position = Vector3.Lerp(origin, target, percent);

            yield return null;
        }

        if (bossStage != BossStages.Critical)
        {
            StartCoroutine(AimAndShoot());
        }
        else
        {
            StartCoroutine(ShootALot());

        }
    }

    IEnumerator AimAndShoot()
    {
        float journey = 0f;

        while (journey <= aiminDuration)
        {
            journey = journey + Time.deltaTime;

            BOSS.transform.up = LerpLookAt2D(BOSS, player);
            yield return null;
        }

        GameObject bulletGO1 = Instantiate(bulletSpear, bulletSpear.transform.position, Quaternion.identity);
        bulletGO1.transform.up = player.transform.position - bulletGO1.transform.position;
        bulletGO1.AddComponent<Projectile_MoveFoward>();
        bulletGO1.tag = "Enemy";

        BOSS.transform.up = player.transform.position - BOSS.transform.position;

        yield return new WaitForSeconds(0.6f);

        GameObject bulletGO2 = Instantiate(bulletSpear, bulletSpear.transform.position, Quaternion.identity);
        bulletGO2.transform.up = player.transform.position - bulletGO2.transform.position;
        bulletGO2.AddComponent<Projectile_MoveFoward>();
        bulletGO2.tag = "Enemy";

        BOSS.transform.up = player.transform.position - BOSS.transform.position;

        yield return new WaitForSeconds(0.6f);

        GameObject bulletGO3 = Instantiate(bulletSpear, bulletSpear.transform.position, Quaternion.identity);
        bulletGO3.transform.up = player.transform.position - bulletGO3.transform.position;
        bulletGO3.AddComponent<Projectile_MoveFoward>();
        bulletGO3.tag = "Enemy";

        BOSS.transform.up = player.transform.position - BOSS.transform.position;

        yield return new WaitForSeconds(2f);
        Hide();

    }

    IEnumerator ShootALot()
    {
        float journey = 0f;

        while (journey <= shootALotDuration)
        {
            journey = journey + Time.deltaTime;

            BOSS.transform.up = LerpLookAt2D(BOSS, player);

            GameObject bulletGO1 = Instantiate(bulletSpear, bulletSpear.transform.position, Quaternion.identity);
            bulletGO1.transform.up = player.transform.position - bulletGO1.transform.position;
            bulletGO1.AddComponent<Projectile_MoveFoward>();
            bulletGO1.tag = "Enemy";
            BOSS.transform.up = player.transform.position - BOSS.transform.position;

            yield return new WaitForSeconds(0.3f);

            BOSS.transform.up = LerpLookAt2D(BOSS, player);

            yield return null;
        }


        yield return new WaitForSeconds(2f);
        Hide();

    }

    public void ShieldOn()
    {
        isAttacking = true;

        switch (Random.Range(1, 3))
        {
            case 1:
                BOSS.transform.position = roofSpawn.transform.position;
                StartCoroutine(MoveToShield(BOSS.transform.position, new Vector2(Random.Range(-22, 22), roofSpawn.transform.position.y - 12f), 2f));
                break;
            case 2:
                BOSS.transform.position = groundSpawn.transform.position;

                StartCoroutine(MoveToShield(BOSS.transform.position, new Vector2(Random.Range(-22, 22), groundSpawn.transform.position.y + 12f), 2f));

                break;
            default:
                BOSS.transform.position = roofSpawn.transform.position;

                StartCoroutine(MoveToShield(BOSS.transform.position, new Vector2(Random.Range(-22, 22), roofSpawn.transform.position.y - 12f), 2f));

                break;
        }

    }

    IEnumerator MoveToShield(Vector2 origin, Vector2 target, float duration)
    {
        float journey = 0f;
        float waitInShield = Random.Range(minWaitShield, maxWaitShield);
        while (journey <= duration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / duration);
            BOSS.transform.position = Vector3.Lerp(origin, target, percent);


            Vector3 toTarget = (player.transform.position - BOSS.transform.position).normalized;

            if (Vector3.Dot(toTarget, BOSS.transform.forward) > 0)
            {
                BOSS.transform.localScale = new Vector3(1, 1, 1);
                Debug.Log("Frente");
            }
            else
            {
                BOSS.transform.localScale = new Vector3(-1, 1, 1);
                Debug.Log("Atras");

            }
            //BOSS.transform.forward = LerpLookAt2D(BOSS, player);

            yield return null;
        }

        shieldStages[0].transform.parent.gameObject.SetActive(true);

        yield return new WaitForSeconds(waitInShield);

        shieldStages[0].transform.parent.gameObject.SetActive(false);
        StartCoroutine(ThrowShield());
    }

    IEnumerator ThrowShield()
    {
        float journey = 0f;
        shieldThrow.transform.up = player.transform.position - shieldThrow.transform.position;
        Vector3 target = shieldThrow.transform.position;
        Transform childThrow = shieldThrow.GetComponentInChildren<SpriteRenderer>().gameObject.transform;

        shieldCollider.enabled = true;

        while (journey <= shieldInAirDuration)
        {
            journey = journey + Time.deltaTime;

            shieldThrow.transform.Translate(transform.up * shieldSpeed * Time.smoothDeltaTime);

            //shieldThrow.transform.position = Vector3.Lerp(origin, target, percent);
            childThrow.Rotate(Vector3.back, 900 * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.2f);

        journey = 0f;
        Vector3 origin = shieldThrow.transform.position;

        while (journey <= shieldInAirDuration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / shieldInAirDuration);

            shieldThrow.transform.position = Vector3.Lerp(origin, target, percent);
            shieldThrow.transform.Rotate(Vector3.back, 900 * Time.deltaTime);

            yield return null;
        }
        shieldCollider.enabled = false;

        yield return new WaitForSeconds(1f);

        Hide();
    }

    private static Vector3 LerpLookAt2D(GameObject origin, GameObject target)
    {
        return Vector3.Lerp(origin.transform.up, (target.transform.position - origin.transform.position), 45);
    }
}
