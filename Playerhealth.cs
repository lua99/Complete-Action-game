using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Playerhealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    //public int damageAmount;

    public static Playerhealth instance;
    public Healthbar healthbar;

    public float immortalTime;
    private float immortalCounter;
    private SpriteRenderer sr;

    private Vector2 checkPoint;

    public GameObject gameOverScreen;


    private void Awake()
    {
        instance = this;
    }

    void Start()
    {

        currentHealth = maxHealth;
        healthbar.SetMaxHealth(currentHealth);
        sr = GetComponent<SpriteRenderer>();
        checkPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (immortalCounter > 0)
        {
            immortalCounter -= Time.deltaTime;
            if (immortalCounter <= 0)
            {
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1f);
            }
        }

    }


    public void DealDamage(int damageAmount)
    {
        if (immortalCounter <= 0)
        {

            currentHealth -= damageAmount;
           
            healthbar.SetHealth(currentHealth);
            if (currentHealth <= 0)
            {
                gameOverScreen.SetActive(true);
                Time.timeScale = 0f;
                //Respawn();
            }

            else
            {
                immortalCounter = immortalTime;
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.6f);
            }
        }

       
    }

    public void UpdatedCheckpoint(Vector2 pos)
    {
        checkPoint = pos;
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        healthbar.SetHealth(currentHealth);
        transform.position = checkPoint;

        gameOverScreen.SetActive(true);

    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

}
