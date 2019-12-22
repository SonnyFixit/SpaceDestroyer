using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : Enemy
{
    public float life = 5f;
    public float birthTime;
    public Vector3[] turnPoints;
    
    
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {


        //Ruch przeciwnika oparty na krzywej Beziera opartej na 3 punktach 

        turnPoints = new Vector3[3];

        //Położenie początkowe
        turnPoints[0] = position;

        float minX = -bounds.width + bounds.radius;
        float maxX = bounds.width - bounds.radius;

        //Losowe położenie w dolnej części ekranu
        Vector3 x;
        x = Vector3.zero;
        x.x = Random.Range(minX, maxX);
        x.y = -bounds.height * Random.Range(2.75f, 2);
        turnPoints[1] = x;

        //Losowe położenie powyżej górnej krawędzi ekranu
        x = Vector3.zero;
        x.y = position.y;
        x.x = Random.Range(minX, maxX);
        turnPoints[2] = x;

        birthTime = Time.time;




        
    }

    public override void Move()
    {
        float z = (Time.time - birthTime) / life;

        if (z > 1)
        {
            Destroy(this.gameObject);
            return;
        }

        Vector3 point1;
        Vector3 point2;
        point1 = (1 - z) * turnPoints[0] + z * turnPoints[1];
        point2 = (1 - z) * turnPoints[1] + z * turnPoints[2];
        position = (1 - z) * point1 + z * point2;
    }
}
