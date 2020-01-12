using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    static public int highScore = 1000;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayerPrefs.HasKey("ScoreManager"))
        {
            highScore = PlayerPrefs.GetInt("ScoreManager");
        }
    }

    // Update is called once per frame
    void Update()
    {
        Text t = this.GetComponent<Text>();
        t.text = "Best score: " + highScore;

        if (highScore > PlayerPrefs.GetInt("ScoreManager"))
        {
            PlayerPrefs.SetInt("ScoreManager", highScore);
        }
    }
}
