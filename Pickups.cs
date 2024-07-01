using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pickups : MonoBehaviour
{
    //public int score;

    public Text scoreText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Gem")
        {
           // Audio.instance.PlaySfx(0);
            Scoring.totalScore++;
            scoreText.text = "SCORE : " + Scoring.totalScore;
            Destroy(collision.gameObject);
            //score++;
            
            Debug.Log(Scoring.totalScore);
        }
    }
}
