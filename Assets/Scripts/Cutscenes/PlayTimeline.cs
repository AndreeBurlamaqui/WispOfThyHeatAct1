using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
public class PlayTimeline : MonoBehaviour
{

    public float rumbleDuration;
    public GameObject MorningAssets, NightAssets, newSpawn;
    [SerializeField] private AnimationStateReference ActivateCutsceneAnimation;

    private void OnEnable()
    {
        CheckDay();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CheckDay();

            if (!SaveManager.Instance.CheckVariable(SaveName.MithraFall))
            {
                ActivateCutsceneAnimation.Animator.enabled = true;

                ActivateCutsceneAnimation.Play();
                SaveManager.Instance.SaveVariable(SaveName.MithraFall, true);
                 
                SaveManager.Instance.SaveVariable(SaveName.CheckpointScene, gameObject.scene.buildIndex);
                SaveManager.Instance.SaveVariable(SaveName.CheckpointSpawnPosition, newSpawn.transform.position);
                SaveManager.Instance.SaveCurrentSlot();

            }
        }
    }

    public void CameraShake()
    {
        CameraManager.Instance.ShakeRumble(CameraManager.ShakeRumbleIntensity.High, rumbleDuration, false);

    }

    public void LastRumble()
    {
        CameraManager.Instance.ShakeRumble(CameraManager.ShakeRumbleIntensity.High, rumbleDuration, false);

    }


    public void CheckDay()
    {

        if (SaveManager.Instance.CheckVariable(SaveName.MithraFall) )
        {
            ActivateCutsceneAnimation.Animator.enabled = false;

            if (!SaveManager.Instance.CheckVariable(SaveName.GotMithra))
            {
                MorningAssets.SetActive(false);
                NightAssets.SetActive(true);
            } 
            else
            {
                MorningAssets.SetActive(true);
                NightAssets.SetActive(false);

            }
        }
        
    }

    
}
