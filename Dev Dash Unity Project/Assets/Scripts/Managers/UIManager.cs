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


    private void Awake()
    {
        PlayerController.OnTakeDamage += UpdateHealthText;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        //Set health text to display player's starting health.
        healthText.text = playerController.StartingHealth.ToString();
        distanceText.text = gameManager.DistanceLeft + " miles";
    }

    // Update is called once per frame
    void Update()
    {
        distanceText.text = gameManager.DistanceLeft.ToString("F2") + " miles";

        //Display win screen if player has won.
        if (gameManager.HasWonGame)
        {
            DisplayEndResultsScreen(winScreen);
        }
        else if(gameManager.IsGameOver)
        {
            DisplayEndResultsScreen(gameOverScreen);
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

    private void UpdateHealthText()
    {
        healthText.text = playerController.Health.ToString();
    }

    private void DisplayEndResultsScreen(GameObject _screen)
    {
        _screen.SetActive(true);
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

    private void OnDisable()
    {
        PlayerController.OnTakeDamage -= UpdateHealthText;
    }
}
