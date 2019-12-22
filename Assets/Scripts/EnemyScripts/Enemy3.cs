using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : Enemy
{

    //Wpływ sinusoidy na ruch statku
    public float eccentricity = 0.5f;
    public float howLong = 8f;

    public Vector3 posStart;
    public Vector3 posEnd;
    public float birthTime;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {

        //Wybierane jest losowe miejsce po lewej stronie ekranu. Wybrany punkt x jest położony poza lewą krawędzia ekranu.
        //-bounds.width oznacza lewą krawędź, bounds.width prawą. -/+ bounds.radius pozwalają przesunąć obiekt poza wybraną krawędź.
        //-bounds.height i bounds.height pozwalają wybrać losową wartość pomiędzy dolną i górną krawędzią ekranu.



        //Określa miejsce po lewej stronie ekranu
        posStart = Vector3.zero;
        posStart.x = -bounds.width - bounds.radius;
        posStart.y = Random.Range(-bounds.height, bounds.height);

        //Określa miejsca po prawej stronie ekranu
        posEnd = Vector3.zero;
        posEnd.x = bounds.width + bounds.radius;
        posEnd.y = Random.Range(-bounds.height, bounds.height);



        //W określonych przypadkach punkt jest przenoszony na drugą stronę
        if (Random.value > 0.5f)
        {
            
            posStart.x *= -1;
            posEnd.x *= -1;
        }

        //Przypisuje bieżący czas
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

        //Interpolacja liniowa dwóch punktów
        position = (1 - x) * posStart + x * posEnd;
    }
}
