using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //This class is in charge of the UI.
    [SerializeField] private Text healthText;
    [SerializeField] private Text distanceText;

    private PlayerController playerController;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject winScreen;
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
        healthText.text = "Health: " + playerController.Health;
        distanceText.text = "Distance Left: \n" + gameManager.DistanceLeft + " miles";

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
    }

    private void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
    }

    private void DisplayWinScreen()
    {
        winScreen.SetActive(true);
  }
}
