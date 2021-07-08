using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyBox;

[RequireComponent(typeof(Collider2D))]
public class HealthManager : MonoBehaviour
{

    [Tag] public string targetTag;
    [ReadOnly][SerializeField] private Animator anim;
    [SerializeField] private AnimationStateReference OnHurtAnimation;
    [SerializeField] private AnimationStateReference OnDeathAnimation;
    public int maxLife;
    [SerializeField][ReadOnly] public int currentLife;
    [SerializeField] private GameObject deathSparkleFX;
    [SerializeField] private Material dissolveMAT;
    [HideInInspector] public bool isPersistent = false;

    [Header("Timers")]
    [ConditionalField(nameof(isThisThePlayer))] [SerializeField] private float maxRestTimer;
    [SerializeField] private float dissolveTimer;
    private float restTimer;


    [Header("Knockback related")]
    [SerializeField] private float knockbackForce;
    public float enemyKnockbackDuration = 0.7f;
    private bool isKnockable = false;

    [SerializeField] private bool isThisThePlayer = false;
    [ConditionalField(nameof(isThisThePlayer))] [SerializeField] private float knockbackDirectionYAxis;
    private Rigidbody2D rb;
    private Collider2D col;

    [SerializeField] private bool isResting = false;
    public bool isDead = false;

    

    public UnityEvent OnHitEvent;
    public UnityEvent OnDeathEvent;
    public UnityEvent OnPersistentEvent;

    private void Start()
    {
        currentLife = maxLife;
        anim = GetComponent<Animator>();

        if (GetComponent<IKnockable>() != null)
            isKnockable = true;

        if (isThisThePlayer)
            rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isResting)
        {
            restTimer -= Time.deltaTime;

            if(restTimer <= 0)
            {
                isResting = false;
            }
        }
    }
    public void ApplyHurt()
    {
        if(!isThisThePlayer)
            currentLife--;

        OnHitEvent.Invoke();

        if (OnHurtAnimation.Assigned)
        {
            OnHurtAnimation.Play();
        }
        CheckDeath();
    }

    IEnumerator DeathDissolve()
    {
        if(deathSparkleFX != null)
            Instantiate(deathSparkleFX, transform.position, Quaternion.identity, transform);

        if (TryGetComponent(out Renderer render))
        {
            Material[] mat = render.materials;

            if (dissolveMAT != null)
            {
                for (int x = 0; x < mat.Length; x++)
                    mat[x] = dissolveMAT;
            }

            float journey = 0f;

            while (journey <= 1f)
            {
                journey += Time.deltaTime / dissolveTimer;
                float percent = Mathf.Lerp(1, 0, journey);

                foreach (Material dissolver in mat)
                {
                    dissolver.SetFloat("_DissolveAmount", percent);
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.5f);
        }

        if (!isPersistent)
        {
            Destroy(gameObject);
        }
        else
        {

            Material[] mat = GetComponent<Renderer>().materials;

                if (dissolveMAT != null)
                {
                    for (int x = 0; x < mat.Length; x++)
                        mat[x] = dissolveMAT;
                }

                foreach (Material dissolver in mat)
                {
                    dissolver.SetFloat("_DissolveAmount", 1);
                }            

            OnPersistentEvent.Invoke();
        }
        
    }

    private void CheckDeath()
    {
        if(currentLife <= 0)
        {
            OnDeathEvent.Invoke();

            if (OnDeathAnimation.Assigned)
                OnDeathAnimation.Play();

            isDead = true;

            //morio
            if (!isThisThePlayer)
            {
                GetComponent<Collider2D>().enabled = false;
                StartCoroutine(DeathDissolve());
            }
            else
            {
         
                SceneLoader.Instance.LoadByCheckpoint();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead)
        {
            if (collision.CompareTag(targetTag))
            {
                if (isThisThePlayer)
                {
                    if (collision.GetComponentInParent<HealthManager>() == null)
                    {
                        isResting = false;
                    }

                    if (!isResting)
                    {

                        if (col == null)
                            col = GetComponent<Collider2D>();

                        Vector2 colCenter = col.bounds.center;
                        Vector2 contactKnockback = collision.ClosestPoint(colCenter);
                        float knockbackDirectionXAxis = -(contactKnockback.x - colCenter.x);
                        float knockbackYAxis = -(contactKnockback.y - colCenter.y);
                        int yDirection = 1;
                        if (Mathf.Sign(knockbackYAxis) != 1)
                        {

                            yDirection = -1;
                        }
                        Debug.Log(knockbackDirectionXAxis);

                        rb.velocity = Vector2.zero;

                        rb.AddForce(new Vector2(knockbackDirectionXAxis, knockbackDirectionYAxis * yDirection) * knockbackForce, ForceMode2D.Impulse);

                        restTimer = maxRestTimer;

                        if (collision.GetComponentInParent<HealthManager>() != null)
                        {
                            isResting = true;
                        }
                        
                        ApplyHurt();

                        
                    }
                }
                else
                {
                    ApplyHurt();

                    if(isKnockable)
                    {
                        if (col == null)
                            col = GetComponent<Collider2D>();

                        float knockbackDirectionXAxis = -(collision.transform.position.x - transform.position.x);

                        StartCoroutine(EnemyKnockback(knockbackDirectionXAxis));
                    }
                }
            }
        }
    }

    private IEnumerator EnemyKnockback(float knockbackDirection)
    {
        float journey = 0;
        IGrounded isGrounded = GetComponent<IGrounded>();

        while(journey <= enemyKnockbackDuration && isGrounded.CheckGround())
        {

            journey += Time.deltaTime;

            transform.Translate( new Vector3(knockbackDirection * knockbackForce * Time.deltaTime,0,0));

            yield return null;
        }
    }
}
