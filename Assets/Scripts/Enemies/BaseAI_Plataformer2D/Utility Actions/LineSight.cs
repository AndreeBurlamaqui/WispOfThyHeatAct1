using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineSight : MonoBehaviour
{
    public Transform headPos;
    public float distanceRay;
    public LayerMask playerLayer;


    /// <summary>
    /// Is on sight? Check by a line raycast at the level of a certain Transform. 
    /// </summary>
    /// <returns>false if not found, true if found player</returns>
    public bool CheckSight()
    {
        RaycastHit2D hit = Physics2D.Raycast(headPos.position, transform.right * transform.localScale.x, distanceRay, playerLayer);
        Debug.DrawRay(headPos.position, transform.right  * distanceRay, Color.red);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
