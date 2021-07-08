using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeenMarabunta : MonoBehaviour, IKnockable
{

    public float maxAtkCD;

    private Patrol patrolScript;
    private LineSight lineSightScript;
    public DashSlash dashSlashScript;
    private float atkCD = 0f;
    private bool isDead = false;
    void Start()
    {
        patrolScript = GetComponent<Patrol>();
        lineSightScript = GetComponent<LineSight>();
        dashSlashScript = GetComponent<DashSlash>();
        GetComponent<HealthManager>().OnDeathEvent.AddListener(delegate { isDead = true; });

        dashSlashScript.OnStartAttack.AddListener(delegate { patrolScript.isOnFreeze = true; });
        dashSlashScript.OnEndAttack.AddListener(delegate { patrolScript.isOnFreeze = false; });

    }
    private void Update()
    {
        if (!isDead)
        {
            if (atkCD > 0f)
            {
                atkCD -= Time.deltaTime;
            }

            if (atkCD <= 0f)
            {

                if (lineSightScript.CheckSight())
                {
                    StartCoroutine(dashSlashScript.Attack());
                    atkCD = maxAtkCD;
                }
            }


            patrolScript.Walk();
        }
    }
}
