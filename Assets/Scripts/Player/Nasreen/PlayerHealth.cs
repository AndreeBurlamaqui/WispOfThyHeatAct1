using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using MyBox;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float slowMoDuration, slowMoFactor;
    public float shakeRumbleDuration;
    public AnimationCurve slowMoCurve;
    private HealthManager _healthManager;
    public GameObject onHitFX;
    [SerializeField] [Tag] private string spikeTag;
    public Light2D blinkLight;
    public float blinkDuration;
    public AnimationCurve blinkCurve;
    private float blinkStart;
    private NasreenController controller;

    //Adelmurgh stuff

    private void Start()
    {
        _healthManager = GetComponent<HealthManager>();
        blinkStart = blinkLight.intensity;
        controller = GetComponent<NasreenController>();
    }

    public void OnPlayerHurt()
    {

        StartCoroutine(ApplySlowMotion());
        StartCoroutine(CameraManager.Instance.ShakeRumble(CameraManager.ShakeRumbleIntensity.Normal, shakeRumbleDuration, false));
        Instantiate(onHitFX, blinkLight.transform.position, Quaternion.identity, transform);

        if (SaveManager.Instance.CheckVariable(SaveName.MetAdelmurgh))
        {

            _healthManager.currentLife--;
            StartCoroutine(ApplyBlinkinRest());
            HUDManager.Instance.UpdateHPUI(_healthManager.currentLife);
           
        }
        else
        {
            controller.canMove = false;
            controller.rb.velocity = Vector2.zero;

            StartCoroutine(OnSpikeHit());
        }
    }

    private IEnumerator ApplyBlinkinRest()
    {
        float journey = 0f;
        while (journey <= blinkDuration)
        {
            journey += Time.deltaTime;

            float percent = Mathf.Clamp01(journey / slowMoDuration);
            float curvePercent = blinkCurve.Evaluate(percent);

            blinkLight.intensity = Mathf.Lerp(blinkStart, 1, curvePercent);

            yield return null;
        }

        blinkLight.intensity = blinkStart;
    }
    private IEnumerator ApplySlowMotion()
    {
        float journey = 0f;
        while (journey <= slowMoDuration)
        {
            journey += Time.unscaledDeltaTime;
            float percent = Mathf.Clamp01(journey / slowMoDuration);

            float curvePercent = slowMoCurve.Evaluate(percent);
            Time.timeScale = Mathf.Lerp(slowMoFactor, 1, curvePercent);
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0, 1);

            yield return null;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.CompareTag(spikeTag))
        {
            if(SaveManager.Instance.CheckVariable(SaveName.MetAdelmurgh))
                _healthManager.ApplyHurt();

            StartCoroutine(OnSpikeHit());
        }*/  
    }

    IEnumerator OnSpikeHit()
    {

        SceneLoader.Instance.FastBlink();

        yield return new WaitForSeconds(0.2f);

        transform.position = controller.lastPos;
        controller.canMove = false;

        yield return new WaitForSeconds(0.7f);
        controller.canMove = true;
    }

}
