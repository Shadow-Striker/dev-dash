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
    [SerializeField] private float gameStartDelay;
    [SerializeField] private bool startGame = false;
    [SerializeField] private int noOfCars;
    [SerializeField] private bool canSpawnCar = true;
    [SerializeField] private float carSpeed = 4;
    [SerializeField] private float maxCarSpeed = 4;

    //Allows other classes to get these variables but not set it.
    //This is to prevent other classes from accidently changing the variable values
    public bool StartGame
    {
        get
        {
            return startGame;
        }
        private set
        {
            startGame = value;
        }
    }

    private bool pauseState = false;
    public bool PauseState
    {
        get
        {
            return pauseState;
        }
        private set
        {
            pauseState = value;
        }
    }

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


    public int NoOfCars
    {
        get
        {
            return noOfCars;
        }
        set
        {
            noOfCars = value;
        }
    }

    public bool CanSpawnCar
    {
        get
        {
            return canSpawnCar;
        }
        set
        {
            canSpawnCar = value;
        }
    }

    public float CarSpeed
    {
        get
        {
            return carSpeed;
        }
        set
        {
            carSpeed = value;
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
        //The spawning of AI cars is delayed for a few seconds
        //to allow the player to prepare and test the controls.
        if(gameStartDelay > 0)
        {
            gameStartDelay -= 1 * Time.deltaTime;
        }
        else
        {
            startGame = true;
        }

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
        if (distanceLeft > 0 && !isGameOver && startGame)
        {
            distanceLeft -= 0.02f * Time.deltaTime;
        }

        if (noOfCars >= 2)
        {
            canSpawnCar = false;
        }
        else
        {
            canSpawnCar = true;
        }

        if(noOfCars < 0)
        {
            noOfCars = 0;
        }

        if (startGame && carSpeed < maxCarSpeed)
            carSpeed += 0.13f * Time.deltaTime;

    }

    //This can be called by other classes to reload the game scene.
    public void ReloadGameScene()
    {
        TogglePauseState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void TogglePauseState()
    {
        //If the game is paused and the player clicks the pause button, unpause the game.
        //Unpause any audio as well.
        //Then set pauseState to false.
        if(pauseState && !isGameOver && !hasWonGame)
        {
            Time.timeScale = 1f;
            AudioListener.pause = false;
            pauseState = false;
        }
        //If the game is not paused and the player clicks the pause button, pause the game.
        //Pause any audio as well.
        //Then set pauseState to true.
        else if (!pauseState && !isGameOver && !hasWonGame)
        {
            Time.timeScale = 0f;
            AudioListener.pause = true;
            pauseState = true;
        }
    }
}
