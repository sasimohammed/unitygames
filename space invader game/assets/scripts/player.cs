using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class player : MonoBehaviour
{



    public AudioSource diesound;

    public Invadors invadorsManager; 
    public GameObject gameOverText;  
    public GameObject restartButton;

    public AudioSource gameoversound;

    public float speed = 5.0f;
    public projectile laserprefab;

    private bool laseractive;

    public AudioSource shootSound;


    public int lives = 3;


    void Start()
    {

        if (gameOverText != null) gameOverText.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
        UpdateLivesUI(); 
    }


    public TextMeshProUGUI livesText;

    void Update()  //chaneg the potsion of our player 
    {
        if (Input.GetKey(KeyCode.RightArrow))//get input for user control 
        {
            transform.position += Vector3.right * speed * Time.deltaTime;

        }
        else  if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) )
        {
           
            shoot();
        }
       

        
    }


    private void shoot()//create our laser using the postion of our player 
    {
        if (!laseractive)
        {

          projectile pro=  Instantiate(laserprefab, transform.position, Quaternion.identity);
            shootSound.Play();

            pro.destroyed += destroyed;//add the first function in our list as this will be called when the shoot trigger

            laseractive = true;

        }

   
        


    }

    private void destroyed()
    {


        laseractive= false; ;

    }

    public void OnTriggerEnter2D(Collider2D collision)//when touch the missile or the inavdor 
    {
        if(collision.gameObject.layer==LayerMask.NameToLayer("invador") || collision.gameObject.layer == LayerMask.NameToLayer("missile"))
        {
            lives--;
            UpdateLivesUI();
            

        }

        if (lives <= 0)//will have the game over
        {

            if (lives <= 0)
            {
                if (invadorsManager != null && !invadorsManager.AllInvadersKilled())
                {
                  
                    if (gameOverText != null) gameOverText.SetActive(true);
                    if (restartButton != null) restartButton.SetActive(true);
                    gameoversound.Play(); ;

                }


                Destroy(gameObject);
            }


            Destroy(gameObject);
            
        }

        else//back  the player agin after a shoot 
        {
            gameObject.SetActive(false);
            Invoke(nameof(Respawn), 1f);
        }

            
    }

    public void Respawn()
    {
        transform.position = new Vector3(-9f, -13f, 0f); 
     
        gameObject.SetActive(true);
        diesound.Play();
    }






    void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = "Lives: " + lives;
        }
    }





}
