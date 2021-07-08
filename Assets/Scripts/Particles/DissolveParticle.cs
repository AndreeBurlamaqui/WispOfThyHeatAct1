
using UnityEngine;
using Cinemachine;
public class DissolveParticle : MonoBehaviour
{

    public float dissolveSpeed;
    public bool shakeScreen = false;

    [SerializeField] private Material material;
    private float dissolveAmount;
    private void Start() {
        material.SetFloat("_DissolveAmount", 1);
        dissolveAmount = 1;

        if (shakeScreen)
        {
            GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    void Update()
    {
        dissolveAmount = Mathf.Clamp(dissolveAmount - Time.deltaTime * dissolveSpeed, 0f, 1f);
        material.SetFloat("_DissolveAmount", dissolveAmount);        
    }
}
