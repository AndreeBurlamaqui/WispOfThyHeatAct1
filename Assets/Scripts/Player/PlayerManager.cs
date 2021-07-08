using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MyBox;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [ReadOnly] [SerializeField] private bool autoAim = false;   
    [ReadOnly] [SerializeField] private PlayerInput _controls;
    [ReadOnly] [SerializeField] private GameObject playerGO;
    private static ControlDeviceType currentControlDevice;
    private HealthManager hm;
    private NasreenController nc;
    private MithraController mc;
    private Rigidbody2D nasreenRb;
    public enum ControlDeviceType
    {
        KeyboardAndMouse,
        Gamepad
    }
    private void Awake()
    {
        Instance = this;
        _controls = FindObjectOfType<PlayerInput>();
        _controls.onControlsChanged += DeviceChanged;
    }

    private void DeviceChanged(PlayerInput obj)
    {
        if (obj.currentControlScheme == "Gamepad")
        {
            if (currentControlDevice != ControlDeviceType.Gamepad)
            {
                currentControlDevice = ControlDeviceType.Gamepad;
            }
        }
        else
        {
            if (currentControlDevice != ControlDeviceType.KeyboardAndMouse)
            {
                currentControlDevice = ControlDeviceType.KeyboardAndMouse;
            }
        }
    }

    public ControlDeviceType WhichDeviceIsBeingUsed
    {
        get => currentControlDevice;
    }

    public bool IsAutoAimEnabled
    {
        get => autoAim;
        set => autoAim = value;
    }


    public int NasreenCurrentLife
    {
        get
        {
            if(hm == null)
                hm = GetPlayerGO.GetComponent<HealthManager>();

            return hm.currentLife;
        }
    }

    public void SetNasreenFullLife()
    {
        if(hm == null)
            hm = GetPlayerGO.GetComponent<HealthManager>();

        hm.currentLife = hm.maxLife;
        hm.isDead = false;
        HUDManager.Instance.UpdateHPUI(hm.maxLife);
    }
    public GameObject GetPlayerGO
    {
        get
        {
            if(playerGO == null)
            {
                playerGO = GameObject.FindWithTag("Player");
            }

            return playerGO;
        }
    }

    public bool MovementState
    {
        get => nc.canMove;

        set
        {
            if (nc == null)
                nc = GetPlayerGO.GetComponent<NasreenController>();

            if (nasreenRb == null)
                nasreenRb = GetPlayerGO.GetComponent<Rigidbody2D>();

            if (mc == null)
                mc = FindObjectOfType<MithraController>();

            nc.canMove = value;
            Debug.Log("can move? " +value);
            if (value == false)
            {
                nasreenRb.velocity = Vector2.zero;
            }

            if (mc != null)
            {
                mc.blockShooting = !value;
            }
        }

    }
}
