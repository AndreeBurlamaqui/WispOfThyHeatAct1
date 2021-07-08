using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFol : MonoBehaviour
{   
    
    private GameObject player;
    public float smoothTime = 0.3F, velocity;
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player == null){
            player = GameObject.FindWithTag("Player");
        } else { 
            transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity, smoothTime),
            transform.position.y,transform.position.z);
        }
    }
}
