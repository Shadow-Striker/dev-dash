using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text healthText;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        healthText.text = "Health : " + playerController.StartingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health : " + playerController.Health;
    }
}
