using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroShip : MonoBehaviour
{

    static public HeroShip HS; // Singleton

    [SerializeField] // Żeby pokazać w inspektorze, tymczasowo
    private float _shield = 1;

    public float restartTime = 2f;


    public float rollMultiplier = -45;
    public float pitchMultiplier = 30;

    public GameObject projectilePrefab;
    public float projectileSpeed = 50f;



    public float shipSpeed = 35;

    //Przechowuje referencję do obiektu gry, który ostatnio uruchomił wyzwalacz
    private GameObject lastTrigger = null;




    void Awake()

    {


        //Nieprzypisywać nic po inicjalizacji

        if (HS == null)
        {
            HS = this;
        }

    }

    // Update is called once per frame
    void Update()
    {

        //Uzyskiwanie informacji

        float axisX = Input.GetAxis("Horizontal");
        float axisY = Input.GetAxis("Vertical");

        Vector3 position = transform.position;
        position.x += axisX * shipSpeed * Time.deltaTime;
        position.y += axisY * shipSpeed * Time.deltaTime;

        transform.position = position;

        //Dodannie małej rotacji, dla ogólnej poprawy dynamiki
        transform.rotation = Quaternion.Euler(axisY * pitchMultiplier, axisX * rollMultiplier, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }


    }

    void Fire()
    {
        GameObject proj = Instantiate<GameObject>(projectilePrefab);
        proj.transform.position = transform.position;
        Rigidbody rb = proj.GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * projectileSpeed;
    }

    void OnTriggerEnter(Collider o)
    {
        Transform rootT = o.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        //Sprawdza, czy to nie jest ten sam obiekt, który wcześniej uruchomił wyzwalacz
        //Jeżeli wartość lastTrigger będzie równa go, to zdarzenie będzie zignorowane lub powtórzone
        //Może się zdarzyć, gdy dwóch potomków tego samego Enemy aktywuje zderzacz statku w tej samej klatce
        if (go == lastTrigger)
        {
            return;
        }

        lastTrigger = go;


        //Sprawdza, czy wróg uderzył w tarczę
        if (go.tag == "Enemy")
        {
            Shield--; //Jeżeli uderzył, poziom tarczy spada o 1
            Destroy(go); // Wróg zostaje zniszczony

        }


    }

    public float Shield 
    {
            get {return (_shield);}
            set {_shield = Mathf.Min(value, 4);
                if (value < 0)
                {

                Destroy(this.gameObject);

                Main.C.GameRestart(restartTime);





                }



            }
        }
    }

