using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class NextSceneHandler : MonoBehaviour
{
    private Collider2D thisCollider;
    [SerializeField] private SceneReference nextScene;
    private int newScene;
    [ReadOnly] public int oldScene;
    [ReadOnly] public bool loadNextScene;
    [ReadOnly] public bool onTarget;

    private bool wasSeen;

    private void Awake()
    {
        oldScene = gameObject.scene.buildIndex;

        newScene = SceneManager.GetSceneByName(nextScene.SceneName).buildIndex;
    }
    private void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        thisCollider.isTrigger = true;

        NextSceneHandler[] checkExists = GameObject.FindWithTag("NextSceneHolder").GetComponentsInChildren<NextSceneHandler>();
        if(checkExists.Length > 0)
        {
            foreach(NextSceneHandler checkHandler in checkExists)
            {

                if (checkHandler.thisCollider.transform == thisCollider.transform)
                {
                    Destroy(gameObject);
                }
            }
        }

        transform.parent = GameObject.FindWithTag("NextSceneHolder").transform;
    }
    private void Update()
    {
        //if (onTarget)
        //{
            if (Camera.main != null)
            {

            if (IsVisible(thisCollider.bounds, Camera.main))
            {
                if (!wasSeen)
                {
                    //if (!loadNextScene)
                    //{
                    //loadNextScene = true;
                    //GameObject onTargetCheckScene = DetectSceneByRay();

                    //if (onTargetCheckScene.scene.buildIndex == oldScene)

                    int? indexPlayerIn = WhichSceneThePlayerIsIn();

                    if (indexPlayerIn.HasValue)
                    {
                        if (indexPlayerIn.Value == oldScene)
                        {
                            SceneLoader.Instance.ApplySceneHandler(newScene, true);
                        }
                        else
                        {
                            SceneLoader.Instance.ApplySceneHandler(oldScene, true);
                        }

                        wasSeen = true;
                    }

                    //GameObject addNextSceneVar = DetectSceneByRay();

                    //if (addNextSceneVar != null)
                    //{
                    //    SceneManagement newSceneToHandleNextScene = addNextSceneVar.GetComponent<SceneManagement>();
                    //    newSceneToHandleNextScene.nextScene = this;
                    //    newSceneToHandleNextScene.CheckNextSceneOnTarget();
                    //}
                    //}
                }
            }
            else if(wasSeen)
            {
                int? indexPlayerIn = WhichSceneThePlayerIsIn();

                if (indexPlayerIn.HasValue)
                {
                    if (indexPlayerIn.Value == oldScene)
                    {
                        SceneLoader.Instance.ApplySceneHandler(newScene, false);
                    }
                    else
                    {
                        SceneLoader.Instance.ApplySceneHandler(oldScene, false);
                    }

                    wasSeen = false;
                }
            }
    }
        //}
        //else
        //{
        //    if (loadNextScene && !IsVisible(thisCollider.bounds, Camera.main))
        //    {
        //        GameObject checkScene = DetectSceneByRay();
        //        if(checkScene != null)
        //        {
        //            if(checkScene.scene.buildIndex == oldScene)
        //            {
        //                SceneLoader.Instance.ApplySceneHandler(newScene, false);
        //            }
        //            else
        //            {
        //                SceneLoader.Instance.ApplySceneHandler(oldScene, false);

        //            }
        //            //SceneManagement newSceneToHandleNextScene = checkScene.GetComponent<SceneManagement>();
        //            //newSceneToHandleNextScene.nextScene = this;
        //            //newSceneToHandleNextScene.CheckNextSceneOnTarget();

        //            loadNextScene = false;
        //            StartCoroutine(RestLoadScene());

        //        }

        //    }
        //}

    }

    IEnumerator RestLoadScene()
    {
        yield return new WaitForSeconds(1f);
        loadNextScene = false;

    }

    int? WhichSceneThePlayerIsIn()
    {
        SceneManagement[] scenes = FindObjectsOfType<SceneManagement>();

        foreach(SceneManagement s in scenes)
        {
            if (s.isPlayerIn)
                return s.gameObject.scene.buildIndex;
        }

        return null;
    }

    GameObject DetectSceneByRay()
    {
        Transform cameraTransform = Camera.main.transform;

        RaycastHit2D detectHit = Physics2D.Raycast(cameraTransform.position, cameraTransform.forward, Mathf.Infinity);

        if (detectHit.collider != null)
        {
            return detectHit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    bool IsVisible(Bounds bounds, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }
}
