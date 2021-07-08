using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDSingleton<T> : MonoBehaviour where T : HUDSingleton<T>
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));

                if (_instance == null)
                {
                    GameObject container = new GameObject("Container HUD Manager");
                    _instance = container.AddComponent<T>();
                }

            }

            return _instance;

        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
        }
    }
}
