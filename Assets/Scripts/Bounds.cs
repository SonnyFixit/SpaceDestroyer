using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{

    public float radius = 1f;


    //Czy skrypt wymusza na obiekcie pozostanie na ekranie, czy pozwala mu wyjść poza ekran
    public bool keepOnScreen = true;


    public float height;
    public float width;

    //Te zmienne definiują kierunek, w którym porusza się obiekt wychodzący poza ekran
    public bool offRight;
    public bool offLeft;
    public bool offUp;
    public bool offDown;



    //Zmienia wartość, jeżeli obiekt przemieszcza się poza ekran
    public bool isPresentOnScreen = true;



    void Awake()
    {
        height = Camera.main.orthographicSize; // ortographicSize zwraca Size z inspektora kamery, height będzie wartością od dołu do góry ekranu
        width = height * Camera.main.aspect; // Uzyskuje odległośćod początku świata do lewej lub prawej krawędzi ekranu


    }


    void LateUpdate()
    {

        isPresentOnScreen = true;


        Vector3 p = transform.position;

        offDown = offUp = offRight = offLeft = false;


        //Jeżeli któraś z tych instrukcji jest spełniona, obiekt gry znajduje się poza obszarem, na którym powinie być

        if (p.x > width - radius)
        {
            p.x = width - radius;
            offRight = true;

        }

        if (p.x < -width + radius)
        {
            p.x = -width + radius;
            offLeft = true;
        }

        if (p.y > height - radius)
        {
            p.y = height - radius;
            offUp = true;

        }

        if (p.y < -height + radius)
        {
            p.y = -height + radius;
            offDown = true;
        }





        isPresentOnScreen = !(offRight || offLeft || offUp || offDown);

        if (keepOnScreen && !isPresentOnScreen)
        {

            transform.position = p;
            isPresentOnScreen = true;
            offRight = offLeft = offUp = offDown = false;
        }

    }

}
