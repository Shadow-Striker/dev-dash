using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text healthText;
    [SerializeField] private Text distanceText;

    private PlayerController playerController;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        gameManager = FindObjectOfType<GameManager>();
        healthText.text = "Health : " + playerController.StartingHealth;
        distanceText.text = "Distance Left: 3";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health : " + playerController.Health;
        distanceText.text = "Distance Left: " + gameManager.DistanceLeft + " miles";
    }
}
