using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using UnityEngine.SceneManagement;

public class Initializator : MonoBehaviour
{

    [SerializeField] private SceneReference[] sceneStarters = new SceneReference[2];


    private void Start()
    {
        foreach(SceneReference ss in sceneStarters)
        {
            ss.LoadSceneAsync(LoadSceneMode.Additive);
        }
    }
}
