using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private float timeLeftBtwnCarSpawns;
    [SerializeField] private float timeBtwnRoadLineSpawns;
    private float timeLeftBtwnRoadLines;
    //time between road line = 0.125
    //Current minTime = 2, maxTime = 4
    [SerializeField] private float minTime, maxTime;
    private GameManager gameManager;
    [SerializeField] private bool spawnCars;

    //Car speed can be read by other classes but it's value cannot be changed by other classes.
    [SerializeField] private float carSpeed;
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
        //Set the time between car spawns to a random value.
        timeLeftBtwnCarSpawns = Random.Range(minTime,maxTime);
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Only start spawning games once the start delay is over.
        if (gameManager.StartGame)
        {
            if (spawnCars)
            {
                SpawnCar();
            }
        }
        
        if(!spawnCars)
        {
            SpawnRoadLine();
        }
    }

    void SpawnCar()
    {
        //Decrease time left between spawns every frame.
        timeLeftBtwnCarSpawns -= Time.deltaTime;

        //if timeLeftBtwnSpawns <= 0 get a inactive car from the object pool and activate it.
        //Then set timeLeftBtwnSpawns to new value.

        if (timeLeftBtwnCarSpawns <= 0)
        {
            if (gameManager.CanSpawnCar)
            {
                GameObject newCar = ObjectPool.SharedInstance.GetPooledCar();

                if (newCar != null)
                {
                    newCar.transform.position = transform.position;

                    newCar.SetActive(true);
                    gameManager.CarsPassed++;
                }
                //gameManager.NoOfCars++;
                timeLeftBtwnCarSpawns = Random.Range(minTime, maxTime + 1);
            }
        }
    }

    void SpawnRoadLine()
    {
        //Decrease time left between spawns every frame.
        timeLeftBtwnRoadLines -= Time.deltaTime;

        //if timeLeftBtwnSpawns <= 0 get a inactive car from the object pool and activate it.
        //Then set timeLeftBtwnSpawns to new value.

        if (timeLeftBtwnRoadLines <= 0)
        {
                GameObject newRoadLine = ObjectPool.SharedInstance.GetPooledRoadLine();

                if (newRoadLine != null)
                {
                    newRoadLine.transform.position = transform.position;

                    newRoadLine.SetActive(true);
                }
            //gameManager.NoOfCars++;
            timeLeftBtwnRoadLines = timeBtwnRoadLineSpawns;
        }
    }


}
