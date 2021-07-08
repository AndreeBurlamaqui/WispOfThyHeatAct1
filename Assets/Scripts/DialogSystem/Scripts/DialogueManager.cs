using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager<T> : MonoBehaviour where T: DialogueManager<T>
{
    //This is the script that will create 

    protected static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<T>();
            
                if(_instance == null)
                {
                    GameObject container = new GameObject("Container Dialogue Manager");
                    _instance = container.AddComponent<T>();
                }
            }

            return _instance;

        }
    }

    private void Awake()
    {
        if(_instance != null)
        {
            Destroy(this);
        } 
    }

    #region Actions
    public event Action<List<Line>,bool, bool, DialogueHolder> OnTalk = delegate { };
    
    public event Action OnFinishTalking;

    public event Action OnSkipLine;

    #endregion

}
