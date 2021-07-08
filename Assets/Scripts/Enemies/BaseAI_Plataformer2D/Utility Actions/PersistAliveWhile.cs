using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistAliveWhile : MonoBehaviour
{
    [SerializeField] private HealthManager hmBoss;
    private GameObject[] startPositionPC;
    [SerializeField] private float respawnYMultiplier, respawnDuration;
    private void Awake()
    {
        HealthManager[] persistenChilds = GetComponentsInChildren<HealthManager>();
        startPositionPC = new GameObject[persistenChilds.Length];
        

        for(int x = 0; x < transform.childCount; x++)
        {
            
            persistenChilds[x].isPersistent = true;
            persistenChilds[x].OnPersistentEvent.AddListener(RespawnPersistenChild);

            startPositionPC[x] = persistenChilds[x].gameObject;

            if (persistenChilds[x].gameObject.isStatic)
                persistenChilds[x].gameObject.isStatic = false;
        }
            
    }

    private void RespawnPersistenChild()
    {
        foreach(GameObject pTC in startPositionPC)
        {
            if (pTC.GetComponent<HealthManager>().currentLife <= 0)
            {
                StartCoroutine(TravelToStartPosition(pTC, pTC.transform.position));

                break;
            }
        }
    }

    IEnumerator TravelToStartPosition(GameObject whichPersistentChild, Vector2 originPos)
    {

        float journey = 0;
        Vector2 multipliedPos = whichPersistentChild.transform.position + (Vector3.up* respawnYMultiplier);
        whichPersistentChild.transform.position = multipliedPos;

        while (journey <= respawnDuration)
        {
            journey += Time.deltaTime;
            float percent = Mathf.Clamp01(journey / respawnDuration);

            whichPersistentChild.transform.position = Vector2.Lerp(multipliedPos, originPos, percent);

            yield return null;
        }

        HealthManager currentHM = whichPersistentChild.GetComponent<HealthManager>();
        currentHM.currentLife = currentHM.maxLife;
        currentHM.isDead = false;
        whichPersistentChild.GetComponent<Collider2D>().enabled = true;
    }


}
