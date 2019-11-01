using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Main : MonoBehaviour
{

    static public Main C;

    public GameObject[] prefabEnemies; //tablica prefabrykatów

    public float spawnPerS = 0.5f; 

    public float defaultPadding = 1.5f; //Wyrównuje położenie nowych statków wroga

    private Bounds b;



     void Awake()
    {
        C = this;

        b = GetComponent<Bounds>();

        //Wywołuje funkcję do 2 sekundy
        Invoke("SpawnEnemy", 1f/spawnPerS);
    }


    public void SpawnEnemy()
    {
        //Wybiera losowy prefabryka z tablic
        int n = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[n]);


        //Umieszcza statek wroga powyżej górnej granicy ekranu, w losowym położeniu na osi x
        float enemyPad = defaultPadding;

        if(go.GetComponent<Bounds>() != null)
        {
            enemyPad = Mathf.Abs(go.GetComponent<Bounds>().radius);


        }

        //Definiuje początkowe położenie statku wroga
        Vector3 p = Vector3.zero;
        float xMin = -b.width + enemyPad;
        float xMax = b.width - enemyPad;
        p.x = Random.Range(xMin, xMax);
        p.y = b.height + enemyPad;
        go.transform.position = p;

        Invoke("SpawnEnemy", 1f / spawnPerS);


    }

    //Wywołuje funkcję Restart, z opóźnieniem równym parametrowi
    public void  GameRestart (float x)
    {
        Invoke("Restart", x);
    }

    public void Restart()
    {
        //Wczytuje ponownie scenę, resetując grę
        SceneManager.LoadScene("SampleScene");
    }

}
