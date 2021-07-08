using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoucingProjectile : MonoBehaviour
{
    public Transform headLevel;
    public Vector2 boxSize;
    public LayerMask collisionMask;
   
    public void ReflectiveShooting(float speed, float distance)
    {

        //raycast to left
        Collider2D hit = Physics2D.OverlapBox(headLevel.position, boxSize, 0f, collisionMask);
        Debug.DrawLine(transform.position, ((transform.right.normalized * -1f) * distance) + transform.position, Color.blue);

        if(hit != null)
        {

            //change rotation z axis
            transform.rotation = Quaternion.Euler(Vector3.forward * Random.Range(25f, 360f));

            Debug.Log("cabesada na pareda");


        }

        //move to left
        transform.position += transform.right * speed * Time.deltaTime * -1f;

    }



}
