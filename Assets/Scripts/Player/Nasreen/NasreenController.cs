using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NasreenController : MonoBehaviour
{
    [Header("Movement")]
	public CharacterController2D controller;
	public float runSpeed = 40f, horizontalMove = 0f;
    public float  dashLongTimer, dashDistance, dashCDTimer, grounded, jumpPressed;
    public bool canMove = true, isSafeArea = true, isSlowFall = false;
    private bool jump = false;
    public Vector3 lastPos;
    public float fallMultiplier = 2.5f;
    public float slowFallMultiplier = -1;
    public float lowJumpMultiplier = 8f;
    public float clampSlowFallVelocity;
    public float fallRecoverTimer;
    public TrailRenderer[] glideTrail = new TrailRenderer[2];

    [SerializeField] private Vector2 safeBoxArea;
    [SerializeField] private LayerMask safeLayerArea;

    [Space(10)]
    private bool hasDoubleJump = true, canDash, isDashing, landFX, isJumpButtonDown, fallRecovered;
    private float dashCD, dashLong, fallingTimer;
    [SerializeField] private float groundedTimer = 0.15f, jumpPressedTimer = 0.2f;
    private float movementResult;
    public Rigidbody2D rb { get; private set; }

    [Space] [SerializeField] private PlayerControl inputControl;
    [HideInInspector] public Animator anim;

    private Audio_Player audioController;

    [Header("Power-Ups")]
    public bool Dash_PowerUp;
    public bool Beam_PowerUp;
    public bool BubbleJump_PowerUp;
    public bool InvDash_PowerUp;

    private void Start() {
        anim = GetComponent<Animator>();
        audioController = GetComponent<Audio_Player>();
        rb = GetComponent<Rigidbody2D>();

        //inputControl.Control_Global.Move.performed += ctx => horizontalMove = Mathf.Round(ctx.ReadValue<float>());
        //inputControl.Control_Global.Move.canceled += ctx => horizontalMove = ctx.ReadValue<float>();

        if (PlayerManager.Instance.IsAutoAimEnabled)
        {
            //Ativado = Trigger
            inputControl.Control_AutoAim.Jump.performed += OnJump;
            inputControl.Control_AutoAim.Jump.canceled += OnJump;


        }
        else
        {
            //Desativado = Normal
            inputControl.Control_Normal.Jump.performed += OnJump;
            inputControl.Control_Normal.Jump.canceled += OnJump;
        }
    }
	void Update () {

        if (anim.GetAnimatorTransitionInfo(0).IsName("AnyState -> Player_Fall") && controller.m_Grounded && !isJumpButtonDown)
        {
            if (anim.GetFloat("Speed") <= 0)
            {
                anim.CrossFade("Player_Idle", 0.1f);
            }
            else
            {
                anim.CrossFade("Player_Run", 0.08f);
            }
        }

        if (canMove){

            // GLIDE //
            if(isJumpButtonDown && rb.velocity.y < 0.1f && !controller.m_Grounded)
            {
                isSlowFall = true;
            }
            else
            {
                isSlowFall = false;
            }
            // GLIDE //

            if (jumpPressed > 0 && isJumpButtonDown)
            {
                jumpPressed -= Time.deltaTime;
            }

            if (controller.m_Grounded)
            {
                grounded = groundedTimer;
            }
            else
            {
                grounded -= Time.deltaTime;
            }

            if ((grounded > 0) && (jumpPressed > 0) && !jump){
                jump = true;

                jumpPressed = 0;
                grounded = 0;

                audioController.SFXByMaterial(AudioType.Jump);

            }
            /*else if(DoubleJump_PowerUp){
                if(hasDoubleJump && (jumpPressed > 0)){
                    rb.velocity = Vector2.zero;
                    hasDoubleJump = false;
                    jump = true;
                    jumpPressed = 0;
                    grounded = 0;
                }
            }*/


        }


        //FALLING
        if (!isSlowFall)
        {
            anim.SetFloat("slowFallAnimationMultiplier", 1f);

            foreach (TrailRenderer trail in glideTrail)
            {
                trail.emitting = false;
            }

            if (rb.velocity.y < 0 && grounded <= 0)
            {
                fallingTimer += Time.deltaTime;

            }
            /*else if (grounded >= 0.1f)
            {

                if (fallingTimer > fallRecoverTimer)
                {
                    canMove = false;
                    anim.SetBool("onRecoverFall", true);
                    fallingTimer -= Time.deltaTime;
                    fallRecovered = false;
                }
                else
                {
                    fallingTimer = 0;
                }

                if (fallingTimer <= 0f && !fallRecovered)
                {
                    fallRecovered = true;
                    canMove = true;
                    anim.SetBool("onRecoverFall", false);
                    fallingTimer = 0;
                }
            }*/
        }
        else
        {
            anim.SetFloat("slowFallAnimationMultiplier", 0.45f);


            if (!glideTrail[0].emitting)
            {
                foreach (TrailRenderer trail in glideTrail)
                {
                    trail.emitting = true;
                }
            }

        }

        if (Dash_PowerUp)
        {
            //Animation Dash
            anim.SetBool("isDashing", isDashing);
            if (canDash)
            {
                if (dashLong <= 0)
                {
                    dashLong = dashLongTimer;
                }

                if (Input.GetButtonDown("Dash"))
                {
                    if (!isDashing)
                    {
                        rb.velocity = Vector2.zero;
                        isDashing = true;

                        if (gameObject.transform.localScale.x != Input.GetAxisRaw("Dash"))
                        {
                            controller.Flip();
                        }

                        //Animation Dash
                        //anim.Play ("Dash", 0, 0);
                    }
                }
            }
            else
            {
                if (dashCD <= 0)
                {
                    dashCD = dashCDTimer;
                    canDash = true;

                }
                else
                {
                    dashCD -= Time.deltaTime;
                }
            }
        }

        //LAST POINT SPIKES
        if (!SaveManager.Instance.CheckVariable(SaveName.MetAdelmurgh))
        { 
            if (controller.m_Grounded && !jump)
            { 
                if (isSafeArea)
                { 
                    if (lastPos != transform.position)
                    {
                        lastPos = transform.position;
                    }
                }

            

                Collider2D[] enemyCol = Physics2D.OverlapBoxAll(transform.position, safeBoxArea, 0f, safeLayerArea);

                if (enemyCol.Length >= 1)
                {
                    if(Array.Exists(enemyCol, c => c.CompareTag("Enemy") || c.gameObject.GetComponent<HealthManager>() != null))
                    {
                        isSafeArea = false;
                    }
                    else
                    {
                        isSafeArea = true;
                    }
                }
            }
        }




        // ANIMATIONS //

        //Animation runnin
        anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        //Animation ground
        anim.SetBool("isGrounded", controller.m_Grounded);

        //Animation falling
        anim.SetFloat("yVelocity", rb.velocity.y);

        // ANIMATIONS //
    }

    private void LateUpdate()
    {
        

        //LAND FX
        if (!controller.m_Grounded)
        {
            if (!landFX)
                landFX = true;

        }
        else
        {
            if (landFX)
                landFX = false;

        }
    }

    void FixedUpdate ()
	{

        if(canMove){


            // RUNNING //
            horizontalMove = Mathf.Round(inputControl.Control_Global.Move.ReadValue<float>());


            if (!isDashing)
                controller.Move(horizontalMove * Time.deltaTime * runSpeed, jump);

            // RUNNING //

            if (jump)
                jump = false;

            // HOLD JUMP//

            if (rb.velocity.y < 0)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * ((isSlowFall ? slowFallMultiplier : fallMultiplier) - 1f) * Time.deltaTime;


                if (isSlowFall)
                {
                    rb.drag = clampSlowFallVelocity;
                }
                else
                {
                    rb.drag = 0.05f;
                }
            }
            else if (rb.velocity.y > 0 && !isJumpButtonDown)
            {
                rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

            // HOLD JUMP //

            //DASH
            if (Dash_PowerUp)
            {
                if (canDash)
                {
                    if (isDashing && !jump)
                    {
                        rb.velocity = transform.right * dashDistance * dashLong * transform.localScale.x;
                        dashLong -= Time.deltaTime;

                        if (dashLong <= 0)
                        {
                            canDash = false;
                            isDashing = false;
                        }
                    }
                }
            }

            /*//DOUBLEJUMP
            if(DoubleJump_PowerUp){
                if(controller.m_Grounded && !hasDoubleJump){
                    hasDoubleJump = true;
                }
            }*/

        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        //if(other.gameObject.name == "SafeArea")
        //    isSafeArea = false;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out UnityEngine.Tilemaps.Tilemap tile))
        {
            if (landFX)
            {
                audioController.SFXByMaterial(AudioType.Land);
                //floorfx.LandingParticle();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D other) {


            if (controller.m_Grounded)
            {

                if (other.gameObject.TryGetComponent(out FloorParticle floorfx))
                {
                    if (horizontalMove != 0)
                    {
                        floorfx.RunParticle(true);
                    }
                    else
                    {
                        floorfx.RunParticle(false);
                    }

                }
            }
        
    }

    private void OnTriggerExit2D(Collider2D other) {
        //if(other.gameObject.name == "SafeArea")
        //    isSafeArea = true;


            if (other.gameObject.TryGetComponent(out FloorParticle floorfx))
            floorfx.RunParticle(false);
        
    }

    public void Talk()
    {
        if (!anim.GetBool("IsTalking"))
        {
            anim.SetBool("IsTalking", true);
        }
        else
        {
            anim.SetBool("IsTalking", false);
        }
    }


    private void Awake()
    {
        inputControl = new PlayerControl();

    }
    private void OnEnable()
    {
        inputControl.Enable();
    }

    private void OnDisable()
    {
        inputControl.Disable();
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.performed)
        {

            if (!isJumpButtonDown && !jump)
            {
                jumpPressed = jumpPressedTimer;
            }

            isJumpButtonDown = true;            
        }

        if (context.canceled)
        {

            isJumpButtonDown = false;
            isSlowFall = false;

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, safeBoxArea);
    }

}
