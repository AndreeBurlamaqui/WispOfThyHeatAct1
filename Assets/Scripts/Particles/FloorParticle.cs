using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorParticle : MonoBehaviour
{

    public bool isBottom, goOffset;
    public GameObject runningParticle,landParticle;


    private GameObject playerPos, runfx;
    private ParticleSystem runPS;

    void Start()
    {


        playerPos = GameObject.FindGameObjectWithTag("Player").transform.Find("GroundCheck").gameObject;
        runfx = Instantiate(runningParticle, playerPos.transform.position, Quaternion.identity, playerPos.transform);
        runPS = runfx.transform.GetChild(0).GetComponent<ParticleSystem>();
        runPS.Stop();
    }

    private void Update() {


    }


    private void OnTriggerStay2D(Collider2D other) {
        if(other.CompareTag("Player")){
            if(isBottom)
                goOffset = true;
        }  
    }



    public void LandingParticle(){
        Instantiate(landParticle, playerPos.transform.position, Quaternion.identity);
    }

    public void RunParticle(bool isActive){
        if(isActive){
            if(!runPS.isPlaying){
                runPS.Play();
            }
        }else{
                runPS.Stop();
        }
    }


}


