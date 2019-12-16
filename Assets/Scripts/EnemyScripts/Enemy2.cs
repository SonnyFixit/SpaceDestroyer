using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : Enemy
{

    //Długość sinusoidy
    public float waveWidth = 6;

    public float rotationY = 45;

    //Czas potrzebny do wykonania pełnego ruchu
    public float frequency = 2;

    //Początkowa wartość położenia X
    private float startPosX;

    private float birthTime;



    // Start is called before the first frame update
    void Start()
    {

        birthTime = Time.time;


        //Przypisuje początkową wartość poożenia statku wroga
        startPosX = position.x;

       
        
    }


    //Zastąpienie metody
    public override void Move()
    {

        //Obsługuje przemieszczanie się w kierunku pionowym
        base.Move();

        Vector3 tempP = position;

        float age = Time.time - birthTime;

        float t = Mathf.PI * 2 * age / frequency;
        float sinus = Mathf.Sin(t);
        tempP.x = startPosX + waveWidth * sinus;
        position = tempP;


        


    }


}
