using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject ship;
    public GameObject[] boards;
    public float scrollScreen = -20;
    public float multiplier = 0.3f;
    public float height;
    public float depth;


    // Start is called before the first frame update
    void Start()
    {

        height = boards[0].transform.localScale.y;
        depth = boards[0].transform.position.z;

        boards[0].transform.position = new Vector3(0, 0, depth);
        boards[1].transform.position = new Vector3(0, height, depth);




    }

    // Update is called once per frame
    void Update()
    {

        float y = 0;
        float x = 0;

        y = Time.time * scrollScreen % height + (height * 0.5f);

        if (ship != null)
        {
            x = -ship.transform.position.x * multiplier;
        }

        boards[0].transform.position = new Vector3(x, y, depth);

        if (y >= 0)
        {

            boards[1].transform.position = new Vector3(x, y - height, depth);

        } else
        {
            boards[1].transform.position = new Vector3(x, y + height, depth);
        }




    }
}
