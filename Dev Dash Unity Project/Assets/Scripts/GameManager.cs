using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private float distanceLeft = 3;

    //Allows other classes to get these variables but not set it.
    //This is to prevent other classes from accidently changing the variable values
    public float DistanceLeft
    {
        get
        {
            return distanceLeft;
        }
        private set
        {
            distanceLeft = value;
        }
    }


    private bool isGameOver = false;
    public bool IsGameOver
    {
        get
        {
            return isGameOver;
        }
        private set
        {
            isGameOver = value;
        }
    }

    [SerializeField] private bool hasWonGame = false;
    public bool HasWonGame
    {
        get
        {
            return hasWonGame;
        }
        private set
        {
            hasWonGame = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
       //Once player's health reaches zero, isGameOver = true.
        if(playerController.Health <= 0)
        {
            isGameOver = true;
        }
        //Once distanceLeft reaches zero, set hasWonGame to true and set distanceLeft to zero
        //in case it's a negative number.
        if(distanceLeft <= 0)
        {
            hasWonGame = true;
            distanceLeft = 0;
        }

        //if distanceLeft is more than zero and the player hasn't lost, keep decreasing distanceLeft every frame.
        if (distanceLeft > 0 && !isGameOver)
        {
            distanceLeft -= 0.02f * Time.deltaTime;
        }
       
    }

    //This can be called by other classes to reload the game scene.
    public void ReloadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
