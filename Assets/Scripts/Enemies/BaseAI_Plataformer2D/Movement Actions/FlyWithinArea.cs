using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FlyWithinArea : MonoBehaviour
{
    public Vector2 areaSize, targetFly;
    public float lerpVelo;
    public float maxTime, top, left, btm, right;

    private Vector3 center;
    private float randomTime;

    void Start()
    {
        center = transform.position;

        Collider2D boundArea = Physics2D.OverlapBox(center, areaSize, 0f);

        Vector2 size = areaSize;
        Vector3 worldPos = transform.TransformPoint(boundArea.offset);

        top = worldPos.y + (size.y / 2f);
        btm = worldPos.y - (size.y / 2f);
        left = worldPos.x - (size.x / 2f);
        right = worldPos.x + (size.x / 2f);



    }

    public void Fly()
    {

        randomTime -= Time.deltaTime;

        if (randomTime <= 0)
        {
            targetFly = new Vector2(Random.Range(left, right), Random.Range(btm, top));
            randomTime = maxTime;
        }

        transform.position = Vector3.Lerp(transform.position, targetFly, Time.deltaTime * lerpVelo);

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, areaSize);
    }

}
