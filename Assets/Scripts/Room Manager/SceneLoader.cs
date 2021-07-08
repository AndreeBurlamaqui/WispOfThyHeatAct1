using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Collections;
using MyBox;
using UnityEngine.Experimental.Rendering.Universal;
using System;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }


    [SerializeField] private SceneReference[] act1Scenes;
    [SerializeField] private SceneReference[] act2Scenes;
    [SerializeField] private SceneReference[] act3Scenes;
    [SerializeField] private SceneReference newGameScene;

    [Space(20)]
    private bool useSpawnPos;
    [SerializeField] [SpriteLayer] int lightSortingLayer;
    [SerializeField] public SceneReference globalClassesScene;
    public Animator loadingScreen;
    [SerializeField] private AnimationStateReference fastBlink;
    [SerializeField] [ReadOnly] private GameObject[] globalLights;

    private void Awake() => Instance = this;

    

    public void ContinueGame()
    {
        useSpawnPos = false;

        LoadByCheckpoint();
    }

    public void NewGame()
    {
        loadingScreen.SetBool("BlinkIn", true);

        useSpawnPos = true;

        StartCoroutine(LoadAsync(act1Scenes, newGameScene.SceneName));
    }

    public void OnSceneLoaded()
    {

        SpawnPlayer();

        UpdateBySceneLoad();

    }
    public void OnSceneUnloaded()
    {
        UpdateBySceneLoad();
    }

    public void ApplySceneHandler(int sceneToActivate, bool activeState)
    {
        //check if scene exists already, if not, this is the another act,
        //so it needs to check wich act it is and then load'em all before enable the correct root objects



            GameObject[] rootGOs = SceneManager.GetSceneByBuildIndex(sceneToActivate).GetRootGameObjects();

            foreach(GameObject toDisable in rootGOs)
            {
                toDisable.SetActive(activeState);
            }

        UpdateBySceneLoad();

            //switch (activeState)
            //{
            //    case true:
            //        OnSceneLoaded();
            //        break;

            //    case false:
            //        OnSceneUnloaded();
            //        break;
            //}
        
    }

    public void LoadByCheckpoint()
    {
        loadingScreen.SetBool("BlinkIn", true);

        SaveManager.Instance.CheckVariable(SaveName.CheckpointScene, out float sceneID);
        Debug.Log("Checkpoint scene build index scene: " + sceneID);

        if (sceneID != float.NaN)
        {
            string continuedSceneName = GetSceneNameFromBuildIndex((int)sceneID);
            Debug.Log("continued scene name: " + continuedSceneName);
            if (Array.Exists(act1Scenes, s => s.SceneName == continuedSceneName))
            {
                StartCoroutine(LoadAsync(act1Scenes, continuedSceneName));
                return;

            }
            else if (Array.Exists(act2Scenes, s => s.SceneName == continuedSceneName))
            {
                StartCoroutine(LoadAsync(act2Scenes, continuedSceneName));
                return;

            }
            else
            {
                StartCoroutine(LoadAsync(act3Scenes, continuedSceneName));
            }
        }
    }

    private IEnumerator LoadAsync(SceneReference[] scenesToLoad, string activatedScene)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(globalClassesScene.SceneName));

        yield return new WaitForSeconds(0.5f);

        for (int x = 0; x < SceneManager.sceneCount; x++)
        {
            string xScene = SceneManager.GetSceneAt(x).name;

            if (xScene != globalClassesScene.SceneName)
            {
                //if (xScene != scenesToLoad.SceneName)
                //{
                    Debug.Log("Unloading " + xScene);
                    SceneManager.UnloadSceneAsync(xScene);
                //}
            }

        }

        for(int x = 0; x < scenesToLoad.Length; x++)
        {
            AsyncOperation asyncLoading = scenesToLoad[x].LoadSceneAsync(LoadSceneMode.Additive);
            asyncLoading.allowSceneActivation = true;
        }

        //AsyncOperation lastSceneLoading = scenesToLoad[0].LoadSceneAsync(LoadSceneMode.Additive);
        //lastSceneLoading.allowSceneActivation = false;

        //while (lastSceneLoading.progress < 0.9f)
        //{
        //    yield return null;
        //}


        yield return new WaitForSeconds(0.5f);

        for (int x = 0; x < SceneManager.sceneCount; x++)
        {
            string xID = SceneManager.GetSceneAt(x).name;

            if(xID != globalClassesScene.SceneName)
            {
                if(xID != activatedScene)
                {
                    GameObject[] rootGO = SceneManager.GetSceneByName(xID).GetRootGameObjects();

                    foreach(GameObject toDisable in rootGO)
                    {
                        toDisable.SetActive(false);
                    }
                }
            }

        }
        yield return new WaitForSeconds(0.5f);

        //lastSceneLoading.allowSceneActivation = true;

        OnSceneLoaded();

    }
   
    public void SpawnPlayer()
    {
        SaveManager.Instance.CheckVariable(SaveName.CheckpointSpawnPosition, out Vector3 spawnPos);

        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            if (!useSpawnPos)
            {

                Instantiate(Resources.Load("Prefabs/Player_Nasreen") as GameObject, spawnPos, Quaternion.identity);

                if (SaveManager.Instance.CheckVariable(SaveName.GotMithra))
                    Instantiate(Resources.Load("Prefabs/Mithra") as GameObject, spawnPos, Quaternion.identity);


                Debug.Log("New Continue Game");


            }
            else
            {
                //primeiro mapa
                Transform spawnPosNG = GameObject.FindWithTag("NGSpawn").transform;

                Instantiate(Resources.Load("Prefabs/Player_Nasreen") as GameObject, spawnPosNG.position, Quaternion.identity);

                int ngBuildIndex = SceneManager.GetSceneByName(newGameScene.SceneName).buildIndex;

                SaveManager.Instance.SaveVariable(SaveName.CheckpointScene, ngBuildIndex);
                SaveManager.Instance.SaveVariable(SaveName.CheckpointSpawnPosition, spawnPosNG.position);

                Debug.Log("New Game");
            }


        }
        else
        {
            FindObjectOfType<NasreenController>().gameObject.transform.position = spawnPos;

            Debug.Log("Continue game by death");

        }

        if (SaveManager.Instance.CheckVariable(SaveName.MetAdelmurgh))
            HUDManager.Instance.SetActiveHPHUD(true);

        //checkpoints
        if (SaveManager.Instance.CheckVariable(SaveName.MetAdelmurgh))
        {
            int maxPlayerLife = GameObject.FindWithTag("Player").GetComponent<HealthManager>().maxLife;
            SaveManager.Instance.SaveVariable(SaveName.LifePoints, maxPlayerLife);
            PlayerManager.Instance.SetNasreenFullLife();
            //If true, then the player WILL have a "Stonepoint", 
            //so, spawn an overlapbox(orsphere) on player's position to see where the "Stonepoint" is
            //and then spawn Adelmurgh on his AdelmurghSit
        }
        else
        {
            SaveManager.Instance.SaveVariable(SaveName.LifePoints, 1);
        }

        SaveManager.Instance.SaveVariable(SaveName.IsDead, false);
        SaveManager.Instance.SaveCurrentSlot();

        if (GameObject.FindGameObjectWithTag("MainCamera") == null)
        {

            GameObject Cam = Instantiate(Resources.Load("Prefabs/Camera") as GameObject, transform.position, Quaternion.identity);
            Cam.GetComponentInChildren<CinemachineVirtualCamera>().m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
            CameraManager.Instance.CurrentCamera = Cam.GetComponentInChildren<CinemachineVirtualCamera>();
        }


        loadingScreen.SetBool("BlinkIn", false);

       

        //SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    public void FastBlink()
    {
        fastBlink.Play();
    }

    private void Start()
    {
        UpdateBySceneLoad();

        if(SceneManager.GetActiveScene() != gameObject.scene)
            SceneManager.SetActiveScene(gameObject.scene);

        if(SceneManager.GetSceneByBuildIndex(0) != null)
        {
            SceneManager.UnloadSceneAsync(0, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        }

    }
    private void UpdateBySceneLoad()
    {
        TilemapManager.Instance.UpdateTilemapArray();
        ResetNextSceneHandlers();

        Debug.Log("Fix Global Light Duplicate detector");
        /*if (!globalLights.IsNullOrEmpty())
        {
            foreach (GameObject gl in globalLights)
            {
                if (gl != null)
                    gl.SetActive(true);
            }

            GameObject[] newGlobalLight = GameObject.FindGameObjectsWithTag("GlobalLight");


            globalLights = new GameObject[newGlobalLight.Length];
            globalLights = newGlobalLight;

            if (Light2DManager.ContainsDuplicateGlobalLight(lightSortingLayer, 0))
            {
                Debug.Log("Duplicated Global Light");

                    globalLights[0].SetActive(true);
                    for (int x = 1; x < globalLights.Length; x++)
                    {
                    Debug.Log("Deactivating " + globalLights[x], globalLights[x]);
                        globalLights[x].SetActive(false);
                    }

            }
        }
        else
        {
            globalLights = GameObject.FindGameObjectsWithTag("GlobalLight");
        }*/
    }


    public static string GetSceneNameFromBuildIndex(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);

        return sceneName;
    }

    public void ResetNextSceneHandlers()
    {
        if(GameObject.FindWithTag("NextSceneHolder") != null)
        {
            NextSceneHandler[] handlers = GameObject.FindWithTag("NextSceneHolder").GetComponentsInChildren<NextSceneHandler>();
            
            foreach(NextSceneHandler nsh in handlers)
            {
                nsh.loadNextScene = false;
            }
        }
    }

}
