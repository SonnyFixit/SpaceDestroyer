using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public AudioClip explosionSound; 
   

    public int s = 200;

    public float speed = 20f;
    public float rateofFire = 0.4f;
    public float health = 10f;

    private HeroShip heroShip;



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



    private void Start()
    {

        ;

        GameObject heroShipObject = GameObject.FindWithTag("Hero");
        if (heroShipObject != null)
        {
            heroShip = heroShipObject.GetComponent<HeroShip>();
        }


    }


    // Update is called once per frame
    void Update()
    {

        

        Move();



        //Czy obiekt przekroczył dolną granicę ekranu. Jeżeli przekroczył, zostaje zniszczony
        if (bounds != null && bounds.offDown)
        {

            Destroy(gameObject);

        }

    }

    private void OnCollisionEnter(Collision c)
    {
        GameObject o = c.gameObject;


        //Odczytuje obiekt gru skojarzony ze zderzaczem, który brał udział w zderzeniu
        //Jeżeli obiekt o zawiera tak HeroProjectiles, zostaje zniszczony razem z daną instancją wroga

        if (o.tag == "HeroProjectiles")
        {

            AudioSource.PlayClipAtPoint(explosionSound, transform.position, 1f);

            Destroy(o); //Niszczy pocisk
            Destroy(gameObject); //Zniszczy statek wroga



            heroShip.AddScore(s);


        }

        


    }
}

