using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarHorn : MonoBehaviour
{
    [SerializeField] private float playSoundDelayLeft;
    private AudioSource audioSource;
    private GameManager gameManager;
    [SerializeField] private AudioClip[] carHorns; 
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
        playSoundDelayLeft = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        PlayCarHorn();
    }

    //Decrease time until next car horn sound can play.
    //Once the playSoundDelayLeft reaches zero and the player hasn't won the game yet, play a car horn sound.
    //After playing, select a random car horn sound form the carHorns array for next time + choose a random delay period.

    private void PlayCarHorn()
    {
        playSoundDelayLeft -= 1 * Time.deltaTime;

        if (playSoundDelayLeft <= 0 && !gameManager.HasWonGame)
        {
            audioSource.Play();
            audioSource.clip = carHorns[Random.Range(0, carHorns.Length)];
            playSoundDelayLeft = Random.Range(1,6);
        }
    }
}
