using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

[RequireComponent(typeof(HealthManager))]
public class AdvancedPatrol : MonoBehaviour
{
    [SerializeField] private AnimationStateReference OnWalkAnimation;
    [SerializeField] private AnimationStateReference OnRotatingAnimation;

    public float speed, rayDistance, rotDuration, freezingDuration;
    public LayerMask groundLayer;
    public bool  isOnFreeze = false, shootRaycast = true;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private bool isCustomAction, isRotating = false;
    private Vector2 initialPosition;
    private Quaternion initialQuaternion;
    public bool isFirst = false;
    private bool waitInLine = false;

    private void Awake()
    {
        if (TryGetComponent(out HealthManager hm))
        {
            hm.OnHitEvent.AddListener(delegate { StartCoroutine(StopWalking()); });
        }
        initialPosition = transform.position;
        initialQuaternion = transform.rotation;
        SceneManagement sm = GetComponentInParent<SceneManagement>();
        sm.OnInvisible.AddListener(GetValidPosition);
        sm.OnVisible.AddListener(ApplyValidPosition);
    }

    private void Update()
    {
        if (!isCustomAction)
        {
            if (!isOnFreeze)
            {
                Walk();
            }
        }
    }
    IEnumerator StopWalking()
    {
        isOnFreeze = true;
        yield return new WaitForSeconds(freezingDuration);
        isOnFreeze = false;
    }

    public void Walk()
    {
        if (!isOnFreeze && !waitInLine)
        {
            if (!isRotating)
                transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (shootRaycast)
            {

                RaycastHit2D checkWallRay = Physics2D.Raycast(transform.position, transform.right, rayDistance, groundLayer);
                RaycastHit2D checkGroundRay = Physics2D.Raycast(groundCheck.position, -transform.up, rayDistance, groundLayer);

                if (checkGroundRay.collider == null)
                {

                    StartCoroutine(Rotate(-90));

                }

                if (checkWallRay.collider != null)
                {
                    StartCoroutine(Rotate(90));
                }
                Debug.DrawRay(transform.position, transform.right * rayDistance);
                Debug.DrawRay(groundCheck.position, -transform.up * rayDistance);
            }
        }

    }

    IEnumerator RestRaycast(){
        shootRaycast = false;
        yield return new WaitUntil(() => !isOnFreeze);
        yield return new WaitForSeconds((float)System.Math.Round(speed / (speed * 2 - 1.15f), 2));
        yield return new WaitUntil(() => !isOnFreeze);
        shootRaycast = true;
        
    }

    IEnumerator Rotate(float rotZTarget)
    {
        float journey = 0f;
        isRotating = true;
        shootRaycast = false;
        Quaternion rotationTarget = transform.rotation * Quaternion.Euler(0, 0, rotZTarget);

        while (journey <= rotDuration)
        {
            journey = journey + Time.deltaTime;
            float percent = Mathf.Clamp01(journey / rotDuration);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, percent);

            yield return null;
        }

        isRotating = false;
        StartCoroutine(RestRaycast());
    }

    private void GetValidPosition()
    {

        RaycastHit2D checkGroundRay = Physics2D.Raycast(groundCheck.position, -transform.up, rayDistance, groundLayer);

        if (checkGroundRay.collider != null && shootRaycast && !isRotating && !waitInLine)
        {

            initialPosition = transform.position;
            initialQuaternion = transform.rotation;

        }

    }

    private void ApplyValidPosition()
    {
        RaycastHit2D checkGroundRay = Physics2D.Raycast(groundCheck.position, -transform.up, rayDistance, groundLayer);

        if (checkGroundRay.collider == null || !shootRaycast || isRotating)
        {
            transform.position = initialPosition;
            transform.rotation = initialQuaternion;
        }

        if (!shootRaycast)
        {
            shootRaycast = true;
            isRotating = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy") && collision.gameObject.TryGetComponent(out AdvancedPatrol ap))
        {
            if(!isFirst)
                ap.isFirst = true;

            if (ap.isFirst && !isRotating)
                waitInLine = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && collision.gameObject.TryGetComponent(out AdvancedPatrol ap))
        {

            if (ap.isFirst)
                waitInLine = false;
        }
    }
}
