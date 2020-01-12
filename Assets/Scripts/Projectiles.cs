using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectiles : MonoBehaviour
{
    

    private Bounds b;

    private void Awake()
    {
        b = GetComponent<Bounds>();
    }

    



    // Update is called once per frame
    void Update()
    {

        //Obiekt, po przekroczeniu górnej granicy ekranu, zostaje zniszczony

        if (b.offUp)
        {
            Destroy(gameObject);
        }
    }

   
    }

