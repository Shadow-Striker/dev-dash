using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    //This class is in charge of the UI.
    [SerializeField] private Text healthText;
    [SerializeField] private Text distanceText;
    [SerializeField] private Text pauseText;
    [SerializeField] private Text carsPassedText;

    private PlayerController playerController;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject pauseScreen;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        //Set health text to display player's starting health.
        healthText.text = "Health : " + playerController.StartingHealth;
        distanceText.text = "Distance Left: \n" + gameManager.DistanceLeft + " miles";
    }

    // Update is called once per frame
    void Update()
    {
        //Update texts every frame.
        healthText.text = "HEALTH: " + playerController.Health;
        distanceText.text = gameManager.DistanceLeft.ToString("F2") + " MILES LEFT";

        //Display win screen if player has won.
        if (gameManager.HasWonGame)
        {
            DisplayWinScreen();
        }

        //Display game over screen if player has lost.
        if(gameManager.IsGameOver)
        {
            DisplayGameOverScreen();
        }

        TogglePauseScreen();

        //Change pause button text based on whether game is paused or not.
        if (gameManager.PauseState)
        {
            pauseText.text = "UNPAUSE";
        }
        else
        {
            pauseText.text = "PAUSE";
        }
    }

    private void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        carsPassedText.gameObject.SetActive(true);
        carsPassedText.text = "CARS PASSED: " + gameManager.CarsPassed;
    }

    private void DisplayWinScreen()
    {
        winScreen.SetActive(true);
        carsPassedText.gameObject.SetActive(true);
        carsPassedText.text = "CARS PASSED: " + gameManager.CarsPassed;
    }

    //Disables or enables pause screen based on gameManager's pause state.
    private void TogglePauseScreen()
    {
        if (gameManager.PauseState)
        {
            pauseScreen.SetActive(true);
        }
        else
        {
            pauseScreen.SetActive(false);
        }
    }
}
