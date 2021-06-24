using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //public static SpawnManager SharedSpawnManager;
    //[SerializeField] private float spawnTime;
    [SerializeField] private float spawnTimeLeft;
    [SerializeField] private Transform[] spawnTransform;
    private GameManager gameManager;
    [SerializeField] private List<Transform> spawnTransformsList;
    // Start is called before the first frame update
    void Start()
    {
        //spawnTimeLeft = Random.Range(1f, 2f);
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.StartGame)
        {
            SpawnCar();
        }
    }

    void SpawnCar()
    {
        spawnTimeLeft -= Time.deltaTime;

        //When it's time to spawn:
        //Choose a random number, 1 or 2.
        //The number chosen is the amount of cars spawned at once.
        //Create list containing each spawnpoint.
        //Randomly select one and remove from list.
        //Randomly select another one.
        // Randomly choose a spawn point from the array.
        // Get a car from the object pool.
        // Spawn car at spawn point.
        // Set new spawn interval.
        if (spawnTimeLeft <= 0)
        {
            List<Transform> tempList = new List<Transform>(spawnTransformsList);
            int randInt = Random.Range(1, 4);
            int noofCarsToSpawn;
            if (randInt == 1)
            {
                noofCarsToSpawn = 2;
            }
            else
            {
                noofCarsToSpawn = 1;
            }
            for (int i = 0; i < noofCarsToSpawn; i++)
            {
                int listItemNumber = Random.Range(0, tempList.Count);
                Vector3 newSpawnPos = tempList[listItemNumber].position;
                GameObject newCar = ObjectPool.SharedInstance.GetPooledCar();

                if (newCar != null)
                {
                    newCar.transform.position = newSpawnPos;
                    newCar.SetActive(true);
                }
                tempList.Remove(tempList[listItemNumber]);
            }
            spawnTimeLeft = Random.Range(1f, 2f);
        }
    }
}
