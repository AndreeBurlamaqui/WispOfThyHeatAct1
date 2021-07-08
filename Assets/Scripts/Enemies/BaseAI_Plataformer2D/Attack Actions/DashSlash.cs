using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyBox;

public class DashSlash : MonoBehaviour
{
    public UnityEvent OnStartAttack, OnEndAttack;
    public float attackDuration, attackDistance;
    public AnimationStateReference attackAnimation;

    public IEnumerator Attack()
    {
        OnStartAttack.Invoke();
        attackAnimation.Play();
        float journey = 0;
        IGrounded isGrounded = GetComponent<IGrounded>();

        while (journey <= attackDuration && isGrounded.CheckGround())
        {

            journey += Time.deltaTime;

            transform.Translate(transform.right * transform.localScale.x * attackDistance * Time.deltaTime);

            yield return null;
        }

        yield return new WaitForSeconds(attackAnimation.Animator.GetCurrentAnimatorClipInfo(0)[0].clip.length);

        OnEndAttack.Invoke();
    }
}
