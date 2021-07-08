using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SineMove : MonoBehaviour
{
    public float frequency, magnitude;
    private Rigidbody2D rb;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        transform.position += transform.right * Mathf.Sin(Time.time * frequency) * magnitude;

        //rb.velocity += new Vector2 (transform.right.x * Mathf.Sin(Time.deltaTime * frequency) * magnitude, transform.right.y * Mathf.Sin(Time.deltaTime * frequency) * magnitude);

        //rb.position += new Vector2(transform.right.x * Mathf.Sin(Time.deltaTime * frequency) * magnitude, transform.right.y * Mathf.Sin(Time.deltaTime * frequency) * magnitude);

    }
}
