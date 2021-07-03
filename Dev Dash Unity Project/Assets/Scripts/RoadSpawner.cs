using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    [SerializeField] private GameObject roadObject;
    private GameObject pooledRoadObject;
    [SerializeField] private float timeLeftBetweenSpawns;
    [SerializeField] private float timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftBetweenSpawns = timeBetweenSpawns;
        GameObject temp;

        temp = Instantiate(roadObject);
        temp.SetActive(false);
        temp = pooledRoadObject;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        //Decrease time left between spawns every frame.
        timeLeftBetweenSpawns -= Time.deltaTime;

        //if timeLeftBtwnSpawns <= 0 get a inactive car from the object pool and activate it.
        //Then set timeLeftBtwnSpawns to new value.

        if (timeLeftBetweenSpawns <= 0)
        {
                GameObject newObject = ObjectPoolTwo.SharedInstance.GetPooledObject();

                if (newObject != null)
                {
                    newObject.transform.position = transform.position;

                    newObject.SetActive(true);
                }
            //gameManager.NoOfCars++;
            timeLeftBetweenSpawns = timeBetweenSpawns;
        }
    }

}
