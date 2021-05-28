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
        if(playerController.Health <= 0)
        {
            isGameOver = true;
        }
        
        if(distanceLeft <= 0)
        {
            hasWonGame = true;
            distanceLeft = 0;
        }

        if (distanceLeft > 0 && !isGameOver)
        {
            distanceLeft -= 0.02f * Time.deltaTime;
        }
       
    }

    public void ReloadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
