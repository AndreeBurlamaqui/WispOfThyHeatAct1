using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using MyBox;

[RequireComponent(typeof(PolygonCollider2D))]
[RequireComponent(typeof(Audio_Area))]


public class SceneManagement : MonoBehaviour
{

    //Inside Scene//
    [SerializeField] public UnityEvent OnVisible, OnInvisible;
    private Transform[] allChilds;
    private int childCount;
    private Collider2D _col;
    private bool wasVisible = false;
    [ReadOnly] public NextSceneHandler nextScene;
    [ReadOnly] public bool isPlayerIn;
    private void Awake()
    {
        _col = GetComponent<Collider2D>();
        childCount = transform.childCount;
        allChilds = new Transform[childCount];

        //if (transform.GetComponentInChildren<NextSceneHandler>() != null)
        //    nextScene = transform.GetComponentInChildren<NextSceneHandler>();

        for (int x = 0; x < childCount; x++)
        {
            Transform currentChild = transform.GetChild(x);

            if (currentChild.GetComponent<NextSceneHandler>() != null)
            {
                nextScene = currentChild.GetComponent<NextSceneHandler>();
            }
            else
            {
                allChilds[x] = currentChild;

                if (!allChilds[x].CompareTag("Global"))
                {
                    allChilds[x].gameObject.SetActive(false);
                }
            }
            

        }

        OnVisible.AddListener(delegate { ApplySetActive(true); });
        OnVisible.AddListener(GetComponent<Audio_Area>().OnEnterArea);

        OnInvisible.AddListener(delegate { ApplySetActive(false); });
        OnInvisible.AddListener(GetComponent<Audio_Area>().OnExitArea);


    }

    private void Update()
    {
        if (Camera.main != null)
        {
            if (IsVisible(_col.bounds, Camera.main))
            {
                if (!wasVisible)
                {
                    wasVisible = true;
                    OnVisible.Invoke();
                }
            }
            else
            {
                if (wasVisible)
                {
                    wasVisible = false;
                    OnInvisible.Invoke();
                }
            }
        }
    }

    bool IsVisible(Bounds bounds, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }


    public void ApplySetActive(bool visibleState)
    {


        for (int x = 0; x < childCount; x++)
        {
            if (allChilds[x] != null)
            {
                if (!allChilds[x].CompareTag("Global"))
                {
                    allChilds[x].gameObject.SetActive(visibleState);
                }
            }
        }

        if(nextScene != null)
        {
            Debug.Log("Next scene detect");
            nextScene.onTarget = visibleState;
        }
    }

    public void CheckNextSceneOnTarget()
    {
        nextScene.onTarget = wasVisible;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isPlayerIn = false;
    }


#if UNITY_EDITOR
    private void Reset()
    {
        GetComponent<PolygonCollider2D>().isTrigger = true;
    }
#endif
}
