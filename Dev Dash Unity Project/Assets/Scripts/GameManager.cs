using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] private GameObject gameOverScreen;
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
            DisplayGameOverScreen();
        }

    }

    private void DisplayGameOverScreen()
    {
        gameOverScreen.SetActive(true);
        IsGameOver = true;
    }

    public void ReloadGameScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
