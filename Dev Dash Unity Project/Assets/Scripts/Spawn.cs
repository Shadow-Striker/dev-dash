using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private float timeLeftBtwnSpawns;
    [SerializeField] private int minTime, maxTime;

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
        timeLeftBtwnSpawns = Random.Range(minTime,maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCar();
    }

    void SpawnCar()
    {
        //Decrease time left between spawns every frame.
        timeLeftBtwnSpawns -= Time.deltaTime;

        //if timeLeftBtwnSpawns <= 0 get a inactive car from the object pool and activate it.
        //Then set timeLeftBtwnSpawns to new value.

        if (timeLeftBtwnSpawns <= 0)
        {
            GameObject newCar = ObjectPool.SharedInstance.GetPooledObject();

            if(newCar != null)
            {
                newCar.transform.position = transform.position;

                newCar.SetActive(true);
            }
            timeLeftBtwnSpawns = Random.Range(minTime, maxTime + 1);
        }
    }
}
