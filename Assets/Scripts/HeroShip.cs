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

    public float dashSpeed = 5f;
    float dashTime = 0.2f;
    public float dashStart = 0.1f;
    byte directionOfDash;

    public float shipSpeed = 35f;

    float enginePower = 3f;
    float maxEnginePower = 3f;

    bool fullSpeedOn;

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
           
            fullSpeedOn = true;

        } else
        {
            
            fullSpeedOn = false;
        }

        if (fullSpeedOn == true)
        {
            shipSpeed = 60f;
            enginePower -= Time.deltaTime;

            if (enginePower <= 0)
            {
                enginePower = 0;
                fullSpeedOn = false;
            }
        } else if (enginePower < maxEnginePower)
        {
            enginePower += Time.deltaTime;
        }

        if (fullSpeedOn == false)
        {
            shipSpeed = 20;
        }

        if (directionOfDash == 0)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                directionOfDash = 1;
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                directionOfDash = 2;
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                directionOfDash = 3;
            }
        }

        else
        {
            if (dashTime <= 0)
            {
                directionOfDash = 0;
                dashTime = dashStart;
                

            } else
            {
                dashTime -= Time.deltaTime;

                if (directionOfDash == 1)
                {
                    transform.position = transform.position + Vector3.left * dashSpeed;
                }
                else if (directionOfDash == 2)
                {
                    transform.position = transform.position + Vector3.right * dashSpeed;
                }
                else if (directionOfDash == 3)
                {
                    transform.position = transform.position + Vector3.up * dashSpeed;
                }
            }
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

