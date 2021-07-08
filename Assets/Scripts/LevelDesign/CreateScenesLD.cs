using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using MyBox;
using UnityEngine.Rendering;

public class CreateScenesLD : MonoBehaviour
{
    [MustBeAssigned] [SerializeField] VolumeProfile scenePostProcessing;
    [MustBeAssigned] [SerializeField] private int howManyScenes;

    [HideInInspector] public bool showPPWarn = false, showSCWarn = false;
    [ConditionalField(nameof(showPPWarn))] [SerializeField] [ReadOnly] string noPPWarn = "Select a Post Processing Volume";
    [ConditionalField(nameof(showSCWarn))] [SerializeField] [ReadOnly] string noSCWarn= "Scene count must be above 1";


    [ButtonMethod]
    private void CreateNewScenes()
    {
        if (scenePostProcessing != null && howManyScenes > 1)
        {
            Volume newVolPP = gameObject.AddComponent(typeof(Volume)) as Volume;
            newVolPP.sharedProfile = scenePostProcessing;
            newVolPP.priority = 1;

            for(int x = 0; x < howManyScenes; x++)
            {
                GameObject newScene = new GameObject("Scene" + (x + 1));
                newScene.transform.SetParent(gameObject.transform);
                newScene.AddComponent(typeof(SceneManagement));
            }

            DestroyImmediate(this);
        }
        else
        {
            if (scenePostProcessing == null)
                showPPWarn = true;

            if (howManyScenes <= 0)
                showSCWarn = true;
        }
    }

}

#endif
