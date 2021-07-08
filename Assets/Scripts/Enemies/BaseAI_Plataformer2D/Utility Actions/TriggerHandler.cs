using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyBox;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerHandler : MonoBehaviour
{

    [MustBeAssigned] [Tag] public string targetTag;
    [SerializeField] public UnityEvent OnEnterTrigger;
    [SerializeField] public UnityEvent OnStayTrigger;
    [SerializeField] public UnityEvent OnExitTrigger;


    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(targetTag))
            OnEnterTrigger.Invoke();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
            OnStayTrigger.Invoke();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
            OnExitTrigger.Invoke();
    }
}
