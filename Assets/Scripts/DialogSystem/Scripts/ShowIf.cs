using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System;

public class ShowIf : MonoBehaviour
{

    [SearchableEnum] public SaveName saveString;

    private Collider2D thisCollider;
    private bool alreadySaw = false;

    void Start()
    {
        thisCollider = GetComponent<Collider2D>();
        
    }

    private void Update()
    {
        if (!alreadySaw)
        {
            if (IsVisible(thisCollider.bounds, Camera.main))
            {
                if (SaveManager.Instance.CheckVariable(saveString))
                {
                    //show
                    for(int x =0; x < transform.childCount; x++)
                    {
                        transform.GetChild(x).gameObject.SetActive(true);

                    }

                }
                else
                {
                    //hide
                    for (int x = 0; x < transform.childCount; x++)
                    {
                        transform.GetChild(x).gameObject.SetActive(false);
                    }

                }

                alreadySaw = true;
            }

        }
        else
        {
            if (!IsVisible(thisCollider.bounds, Camera.main) && alreadySaw)
            {
                alreadySaw = false;
            }
        }
    }

    bool IsVisible(Bounds bounds, Camera camera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, bounds);
    }

}
