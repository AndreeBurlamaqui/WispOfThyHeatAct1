using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[ExecuteAlways]
public class SpriteShapeDetail : MonoBehaviour
{

    [Range(16, 128)]
    public int m_SpriteShapeDetail;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var sc = GetComponent<SpriteShapeController>();
        if (sc)
            sc.splineDetail = m_SpriteShapeDetail;
    }
}
