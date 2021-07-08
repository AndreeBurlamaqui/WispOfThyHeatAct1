using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MithraCutscene : MonoBehaviour
{
    public float frequency, magnitude, farVel;
    public bool finalMove;

    private Vector3 pos;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");

        pos = transform.position;
    }

    // Update is called once per frame

    private void FixedUpdate()
    {


        if (finalMove)
        {
            pos = Vector3.Lerp(transform.position, player.transform.position, farVel);
            frequency = 2;
            magnitude = 0.2f;
            transform.position = pos + player.transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }
        else
        {
            transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;
        }

    }

   

}
