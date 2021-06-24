using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region OBJECT POOLING INFO
    //This class for object pooling
    //Object pooling is a design pattern that is used for optimisation,
    //particularly for mobile games.
    //It works by pre-instantiating objects into a list before the game starts, then
    //disabling them until the game requires them.
    //This is a better approach as instantiating and destroying objects can severely
    //impact performance.
    //For this game I have used it for the cars, as I have them spawning and despawning
    //relatively quickly.
    #endregion
    
     //Single instance of object pool
    public static ObjectPool SharedInstance;
    //Stores a list of all the gameObjects that have been pre-instantiated.
    public List<GameObject> pooledCars;
    public List<GameObject> pooledRoadLines;
    public GameObject[] objectToPool;
    //Number of objects to pool.
    public int noOfCarsToPool;
    public int noOfRoadLinesToPool;
    private void Awake()
    {
        //Setting ObjectPool variable, (which is null on initialisation) to this script.
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setting list of pooled objects to a new list.
        pooledCars = new List<GameObject>();
        GameObject temp;


        ///This code creates a set of pre-instantiated gameObjects to use before the game even starts.
        //1. Instantiate a new car
        //2. Deactivate it for later use
        //3. Add the new car to the list of pooledCars.
        //4. Repeat 1 to 3 for the same amount of times as noOfCarsToPool.
        for (int i = 0; i < noOfCarsToPool; i++)
        {
            temp = Instantiate(objectToPool[0]);
            temp.SetActive(false);
            pooledCars.Add(temp);
        }


        //1. Instantiate a new road line
        //2. Deactivate it for later use
        //3. Add the new car to the list of pooledRoadLines.
        //4. Repeat 1 to 3 for the same amount of times as noOfRoadLinesToPool.
        for (int i = 0; i < noOfRoadLinesToPool; i++)
        {
            temp = Instantiate(objectToPool[1]);
            temp.SetActive(false);
            pooledRoadLines.Add(temp);
        }
    }

    //This function can be called by other classes when a car needs to be spawned.
    //Replaces Unity's Instantiate function.
    public GameObject GetPooledCar()
    {
        for(int i = 0; i < noOfCarsToPool; i++)
        {
            //Only return cars that are not currently being used.
            //else return null.
            if (!pooledCars[i].activeInHierarchy)
            {
                return pooledCars[i];
            }
        }
        return null;
    }

    public GameObject GetPooledRoadLine()
    {
        for (int i = 0; i < noOfRoadLinesToPool; i++)
        {
            //Only return cars that are not currently being used.
            //else return null.
            if (!pooledRoadLines[i].activeInHierarchy)
            {
                return pooledRoadLines[i];
            }
        }
        return null;
    }
}
