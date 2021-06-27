﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private Vector3 direction;
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private float speedModifier = 1f;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Color[] colours;
    [SerializeField] private bool isVisible = false;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.color = colours[Random.Range(0, colours.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //If player hasn't won game yet, move the car.
        //And increase the speed slightly every frame until the speed reaches 7.
        //This is to make the game gradually harder.

        if (!gameManager.HasWonGame)
        {
            Movement();
           // if (speed < 8.5f)
                //speed += 0.002f;
        }
    }

    //Moves car downwards every frame.
    void Movement()
    { 
        transform.position += gameManager.CarSpeed * speedModifier * direction.normalized * Time.deltaTime;
    }

    //Deactivates car once it is no longer visible by the camera.
    private void OnBecameInvisible()
    {
        if (transform.position.y > Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y) return;
        isVisible = false;
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        spriteRenderer.color = colours[Random.Range(0, colours.Length)];
        //gameManager.NoOfCars--;
        gameObject.SetActive(false);
        print("Car has gone off bottom of screen.");
    }

    private void OnBecameVisible()
    {
        isVisible = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When colliding with the player, get their IDamagable interface.
        //If there is an IDamagable interface the player will take damage.

        if (collision.gameObject.tag == "Player")
        {
            IDamagable playerDamagable = collision.gameObject.GetComponent<IDamagable>();

            if(playerDamagable != null && !playerDamagable.DamageImmunity)
            {
                playerDamagable.TakeDamage(1);
            }
        }

        if (collision.gameObject.tag == "TwoCarsOnlyArea")
        {
            gameManager.NoOfCars++;
        }

        if (collision.gameObject.tag == "TwoCarsOnlyArea" && gameManager.NoOfCars > 2 && !isVisible)
        {
            print("(DEACTIVATING BTW) NO. OF CARS LOL: " + gameManager.NoOfCars);
            //gameManager.NoOfCars--;
            //gameObject.SetActive(false);
        }

    }

    /*private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwoCarsOnlyArea" && gameManager.NoOfCars > 2)
        {
            print("(DEACTIVATING BTW) NO. OF CARS LOL: " + gameManager.NoOfCars);
            gameManager.NoOfCars--;
            gameObject.SetActive(false);
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TwoCarsOnlyArea")
        {
            gameManager.NoOfCars--;
        }
    }

    /*private void OnEnable()
    {
        //Adjusts the speed of the AI car when spawned.
        int randInt = Random.Range(0, 2);
        if (randInt == 0)
        {
            speedModifier = 1f;
        }
        else
        {
            speedModifier = 1.1f;
        }
    }*/
}
