using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnObject;
    [SerializeField] private float timeBtwnSpawns;
    [SerializeField] private float timeLeftBtwnSpawns;

    // Start is called before the first frame update
    void Start()
    {
        timeLeftBtwnSpawns = Random.Range(2f,5f);
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCar();
    }

    void SpawnCar()
    {
        timeLeftBtwnSpawns -= Time.deltaTime;

        if (timeLeftBtwnSpawns <= 0)
        {
            GameObject newCar = ObjectPool.SharedInstance.GetPooledObject();

            if(newCar != null)
            {
                newCar.transform.position = transform.position;
                newCar.SetActive(true);
            }
            timeLeftBtwnSpawns = Random.Range(2f, 5f);
        }
    }
}
