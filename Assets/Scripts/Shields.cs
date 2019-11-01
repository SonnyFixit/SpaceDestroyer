using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shields : MonoBehaviour
{

    public float rotation = 0.1f;

    public int shieldLevel = 1;

    Material mat;

    // Start is called before the first frame update
    void Start()
    {
        mat = GetComponent<Renderer>().material; 
    }

    // Update is called once per frame
    void Update()
    {
        int currentShieldLevel = Mathf.FloorToInt(HeroShip.HS.Shield); //Odczytuje poziom tarczy

        if (shieldLevel != currentShieldLevel)
        {

            //Wybiera poziom tekstury odpowiadający tarczy

            shieldLevel = currentShieldLevel;

            mat.mainTextureOffset = new Vector2(0.2f * shieldLevel, 0);

        }

            
        //Obrót tarczy

            float r = (rotation * Time.time * 360);
            transform.rotation = Quaternion.Euler(0, 0, r);
        
    }
}
