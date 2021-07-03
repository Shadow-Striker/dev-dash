using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTwo : MonoBehaviour
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
    public static ObjectPoolTwo SharedInstance;
    //Stores a list of all the gameObjects that have been pre-instantiated.
    public List<GameObject> pooledObjects;
    public GameObject[] objectToPool;
    //Number of objects to pool.
    public int noOfObjectsToPool;
    private void Awake()
    {
        //Setting ObjectPool variable, (which is null on initialisation) to this script.
        SharedInstance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setting list of pooled objects to a new list.
        pooledObjects = new List<GameObject>();
        GameObject temp;


        ///This code creates a set of pre-instantiated gameObjects to use before the game even starts.
        //1. Instantiate a new object
        //2. Deactivate it for later use
        //3. Add the new object to the list of pooledObjects.
        //4. Repeat 1 to 3 for the same amount of times as noOfCarsToPool.
        for (int i = 0; i < noOfObjectsToPool; i++)
        {
            temp = Instantiate(objectToPool[0]);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    //This function can be called by other classes when a car needs to be spawned.
    //Replaces Unity's Instantiate function.
    public GameObject GetPooledObject()
    {
        for (int i = 0; i < noOfObjectsToPool; i++)
        {
            //Only return cars that are not currently being used.
            //else return null.
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
