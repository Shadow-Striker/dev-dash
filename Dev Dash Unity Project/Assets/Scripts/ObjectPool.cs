using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedObjectPool;
    //Stores a list of all the gameObjects that have been preloaded.
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    //Number of objects to pool.
    public int amountToPool;

    private void Awake()
    {
        //Setting ObjectPool variable, (which is null on initialisation) to this script.
        SharedObjectPool = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //Setting list of pooled objects to a new list.
        pooledObjects = new List<GameObject>();
        GameObject temp;

        //1. Instantiate a new object
        //2. Deactivate it for later use
        //3. Add the new gameObject to the list of pooledObjects.
        //4. Repeat 1 to 3 for the same amount of times as amountToPool.
        for(int i = 0; i < amountToPool; i++)
        {
            temp = Instantiate(objectToPool);
            temp.SetActive(false);
            pooledObjects.Add(temp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
