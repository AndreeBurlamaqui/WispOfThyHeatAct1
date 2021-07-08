using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using MyBox;

public class Checkpoint : MonoBehaviour
{
    [Separator("Rock Animations")]
    public AnimationStateReference IdleRockAnimation;
    public AnimationStateReference InactiveAnimation, SavingAnimation;

    [Separator("Tooltip Animations")]
    public AnimationStateReference showTooltip;
    public AnimationStateReference hideTooltip;

    [Space(50f)]
    [SerializeField] private Transform spawnPoint;

    private PlayerControl inputControl;
    private bool metAdelmurgh = false, canSave = false, alreadySaw = false;
    private Collider2D thisCollider;

    void Start()
    {
        thisCollider = GetComponent<Collider2D>();

    }
    void Update()
    {
        if (!alreadySaw)
        {
            if (IsVisible(thisCollider.bounds, Camera.main))
            {
                metAdelmurgh = SaveManager.Instance.CheckVariable(SaveName.MetAdelmurgh);


                if (metAdelmurgh)
                {
                    SaveManager.Instance.CheckVariable(SaveName.CheckpointSpawnPosition, out Vector3 checkScene);

                    if (checkScene != Vector3.negativeInfinity)
                    {
                        if (Vector3.Distance(spawnPoint.position, checkScene) <= 1)
                        {
                            if (IdleRockAnimation.Assigned)
                                IdleRockAnimation.Play();


                        }
                    }
                }
                else
                {
                    InactiveAnimation.Play();
                }

                alreadySaw = true;

            }
        }
        else
        {
            if (!IsVisible(thisCollider.bounds, Camera.main) && alreadySaw)
            {
                alreadySaw = false;
            }
        }
        
    }

    bool IsVisible(Bounds bounds, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

    #region Input Control
    private void Awake()
    {
        inputControl = new PlayerControl();

        inputControl.Control_Global.Interact.performed += x => Save();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }
    #endregion


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (metAdelmurgh)
        {
            if (collision.CompareTag("Player"))
            {
                canSave = true;

                if (showTooltip.Assigned)
                    showTooltip.Play();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (metAdelmurgh)
        {
            if (collision.CompareTag("Player"))
            {
                canSave = false;

                if (hideTooltip.Assigned)
                    hideTooltip.Play();
            }
        }

        
    }


    public void Save()
    {
        if (canSave)
        {
            //Set Animation
            if (SavingAnimation.Assigned)
                SavingAnimation.Play();

            //Set to playerPrefs
            SaveManager.Instance.SaveVariable(SaveName.CheckpointScene, gameObject.scene.buildIndex);
            SaveManager.Instance.SaveVariable(SaveName.CheckpointSpawnPosition, spawnPoint.position);
            SaveManager.Instance.SaveVariable(SaveName.IsDead, 0f);

            //Restore Player Health
            PlayerManager.Instance.SetNasreenFullLife();
            SaveManager.Instance.SaveVariable(SaveName.LifePoints, PlayerManager.Instance.NasreenCurrentLife);

            SaveManager.Instance.SaveCurrentSlot();

            Debug.Log("Saved!");

            if (hideTooltip.Assigned)
                hideTooltip.Play();
        }
    }

    public void SetHPUIActiveAfterTalk()
    {
        canSave = true;
        HUDManager.Instance.SetActiveHPHUD(true);
        Save();
    }
}
