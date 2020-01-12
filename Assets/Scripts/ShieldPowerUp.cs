using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerUp : MonoBehaviour
{


    public float speed = 20f;




    protected Bounds bounds;

    public Vector3 position
    {
        get { return (this.transform.position); }

        set { this.transform.position = value; }
    }


    //Pobiera pozycję obiektu, zmiejsza ją w osi Y i przypisuje do position
    public virtual void Move()
    {
        Vector3 tempPosition = position;
        tempPosition.y -= speed * Time.deltaTime;
        position = tempPosition;
    }

    void Awake()
    {
        bounds = GetComponent<Bounds>();
    }


    // Update is called once per frame
    void Update()
    {



        Move();

        transform.Rotate(2, 0, 2);

        //Czy obiekt przekroczył dolną granicę ekranu. Jeżeli przekroczył, zostaje zniszczony
        if (bounds != null && bounds.offDown)
        {

            Destroy(gameObject);

        }

    }




}


   

