using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{

    public float eccentricity = 0.5f;
    public float howLong = 8f;

    public Vector3 posStart;
    public Vector3 posEnd;
    public float birthTime;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {

        posStart = Vector3.zero;
        posStart.x = -bounds.width - bounds.radius;
        posStart.y = Random.Range(-bounds.height, bounds.height);

        posEnd = Vector3.zero;
        posEnd.x = bounds.width + bounds.radius;
        posEnd.y = Random.Range(-bounds.height, bounds.height);

        if (Random.value > 0.5f)
        {
            posStart.x *= -1;
            posEnd.x *= -1;
        }

        birthTime = Time.time;


    }

    public override void Move()
    {
        float x = (Time.time - birthTime) / howLong;


        if (x > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        x = x + eccentricity * (Mathf.Sin(x * Mathf.PI * 2));

        position = (1 - x) * posStart + x * posEnd;
    }
}
