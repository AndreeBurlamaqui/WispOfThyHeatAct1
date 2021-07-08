using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MithraController : MonoBehaviour
{

    
    
    public float canShootMax, farVel, closeVel, dist, frequency, magnitude;
    public bool blockShooting = false, autoAim, onRange = false, canShoot = false;
    public float amplitudeShoot, frequencyShoot, shootKickDuration, shootKickForce;
    public GameObject fireRange, enemyOnRange;
    public Transform muzzlePosition;

    [SerializeField] private AnimationCurve shootKickCurve;        
    private Transform backPosition;
    private GameObject bullet, muzzle;
    private float canShootTimer, nasreenDir;
    private bool canMove = true, canLook = true, onHold = false;
    private Vector3 pos;
    private Vector2 crossDir, dir, mousePosition, thumbstickDirection;

    [Space] [SerializeField] private PlayerControl inputControl;

    void Start()
    {
        backPosition = GameObject.Find("MithraPos").transform;
        bullet = Resources.Load("Prefabs/BulletMithra") as GameObject;
        muzzle = Resources.Load("Prefabs/FX/MuzzleMithra") as GameObject;

        inputControl.Control_Global.Move.performed += ctx => nasreenDir = ctx.ReadValue<float>();

        AutoAimCheck(PlayerManager.Instance.IsAutoAimEnabled);

        if (autoAim)
        {
            //Ativado = Trigger
            inputControl.Control_AutoAim.MithraFire.performed += ctx => onHold = true;
            inputControl.Control_AutoAim.MithraFire.canceled += ctx => onHold = false;
        }
        else
        {
            //Desativado = Normal
            inputControl.Control_Normal.MithraFire.performed += ctx => onHold = true;

            inputControl.Control_Normal.Aim.performed += OnAim;
            inputControl.Control_Normal.Aim.canceled += OnAim;

            inputControl.Control_Normal.MithraFire.canceled += ctx => onHold = false;
        }

    }

    private void Awake()
    {
        inputControl = new PlayerControl();
    }

    private void OnEnable()
    {
        inputControl.Enable();

        if (gameObject.scene != UnityEngine.SceneManagement.SceneManager.GetActiveScene())
            UnityEngine.SceneManagement.SceneManager.MoveGameObjectToScene(gameObject, UnityEngine.SceneManagement.SceneManager.GetActiveScene());

    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    private void Update()
    {
        ShootingDetector();
    }
    private void FixedUpdate() {

        #region Movement Related
        //Movement
        if (canMove){
            pos = Vector3.Lerp(transform.position, backPosition.position, farVel);

            transform.position = pos + backPosition.up * Mathf.Sin(Time.time * frequency) * magnitude;
 
        }
        #endregion 

        #region Shooting cooldown
        //Fire cooldown
        if (!canShoot)
        {
            canShootTimer -= Time.deltaTime;
            if (canShootTimer <= 0)
            {
                canShoot = true;
            }
        }
        #endregion

        //Dashing

    }

    public void OnAim(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            thumbstickDirection = context.ReadValue<Vector2>(); 
            onHold = true;
        }

        if (context.canceled)
        {
            thumbstickDirection = Vector2.zero;
            onHold = false;
        }

    }
    public void ShootingDetector()
    {
        CameraManager.Instance.ShakeRumble(CameraManager.ShakeRumbleIntensity.Low, shootKickDuration, true);

        #region Shooting by Normal Control Scheme
        if (!autoAim)
        {
            switch (PlayerManager.Instance.WhichDeviceIsBeingUsed)
            {
                #region Shooting by gamepad
                case PlayerManager.ControlDeviceType.Gamepad:

                    if (onHold)
                    {
                        //Cutscene purpose
                        if (!blockShooting)
                        {
                            if (canShoot)
                            {
                                canMove = false;
                                canLook = false;

                                StartCoroutine(KickShoot(transform.position));

                                GameObject bulletGO = Instantiate(bullet, muzzlePosition.position, Quaternion.identity);
                                GameObject muzzleGO = Instantiate(muzzle, muzzlePosition.position, Quaternion.identity);

                                //LookAt Thumbstick Direction
                                float gangle = Mathf.Atan2(thumbstickDirection.y, thumbstickDirection.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.AngleAxis(gangle + 90f, Vector3.forward);

                                bulletGO.transform.rotation = Quaternion.AngleAxis(gangle - 90f, Vector3.forward);
                                muzzleGO.transform.rotation = Quaternion.AngleAxis(gangle, Vector3.forward);



                                 canShoot = false;
                                canShootTimer = canShootMax;

                            }


                        }
                    }
                    else if (canLook)
                    {
                        transform.rotation = Quaternion.AngleAxis(nasreenDir * 90f, Vector3.forward);
                    }

                    break;

                #endregion

                #region Shooting by Mouse
                case PlayerManager.ControlDeviceType.KeyboardAndMouse:

                    if (onHold)
                    {
                        //Cutscene purpose
                        if (!blockShooting)
                        {
                            if (canShoot)
                            {
                                 canMove = false;
                                 canLook = false;

                                StartCoroutine(KickShoot(transform.position));

                                GameObject bulletGO = Instantiate(bullet, muzzlePosition.position, Quaternion.identity);
                                 GameObject muzzleGO = Instantiate(muzzle, muzzlePosition.position, Quaternion.identity);

                                //LookAt MousePos
                                Vector2 dir = Mouse.current.position.ReadValue() - new Vector2(Camera.main.WorldToScreenPoint(transform.position).x, Camera.main.WorldToScreenPoint(transform.position).y);
                                float mAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                                transform.rotation = Quaternion.AngleAxis(mAngle + 90f, Vector3.forward);

                                bulletGO.transform.rotation = Quaternion.AngleAxis(mAngle - 90f, Vector3.forward);
                                 muzzleGO.transform.rotation = Quaternion.AngleAxis(mAngle, Vector3.forward);                                

                                 canShoot = false;
                                 canShootTimer = canShootMax;

                            }


                        }
                    } else if (canLook)
                    {
                        Vector2 dir = Mouse.current.position.ReadValue() - new Vector2(Camera.main.WorldToScreenPoint(transform.position).x, Camera.main.WorldToScreenPoint(transform.position).y);
                        float mAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.AngleAxis(mAngle + 90f, Vector3.forward);
                    }

                    break;
                    #endregion
            }
        }
        #endregion

        #region Shooting by AutoAim Control Scheme
        else
        {
            if (onHold)
            {
                //Cutscene purpose
                if (!blockShooting)
                {
                    if (onRange && canShoot)
                    {
                        canMove = false;
                        canLook = false;

                        StartCoroutine(KickShoot(transform.position));

                        Debug.Log("Shoot Autoaim");

                        GameObject bulletAutoGO = Instantiate(bullet, muzzlePosition.position, Quaternion.identity);
                        GameObject muzzleAutoGO = Instantiate(muzzle, muzzlePosition.position, Quaternion.identity);

                        //LookAt Aim
                        Vector3 dir = enemyOnRange.transform.position - transform.position;
                        dir.Normalize();
                        float autoAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                        muzzleAutoGO.transform.rotation = Quaternion.AngleAxis(autoAngle, Vector3.forward);

                        transform.rotation = Quaternion.AngleAxis(autoAngle + 90f, Vector3.forward);

                        bulletAutoGO.transform.up = enemyOnRange.transform.position - transform.position;


                    }
                }
            } else if (canLook)
            {
                transform.rotation = Quaternion.AngleAxis(nasreenDir * 90f, Vector3.forward);
            }
        }

        #endregion

    }




    public void MoveNLook(){
        canMove = true;
        canLook = true;
        //anim.Play("Nothing", 0);
    }

    public void AutoAimCheck(bool state)
    {
        Debug.Log("Autoaim is " + state);

        fireRange.SetActive(state);
        autoAim = state;
    }

    public IEnumerator KickShoot(Vector3 origin)
    {
        float journey = 0f;
        while (journey <= shootKickDuration)
        {
            journey += Time.deltaTime;
            float percent = Mathf.Clamp01(journey / shootKickDuration);

            float curvePercent = shootKickCurve.Evaluate(percent);
            transform.position = Vector3.LerpUnclamped(origin, origin + (transform.up * shootKickForce), curvePercent);

            yield return null;
        }

        MoveNLook();

    }


}
