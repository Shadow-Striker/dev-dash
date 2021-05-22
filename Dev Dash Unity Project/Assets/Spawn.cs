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
        timeLeftBtwnSpawns = timeBtwnSpawns;
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
            Instantiate(spawnObject, transform);
            timeLeftBtwnSpawns = timeBtwnSpawns;
        }
    }
}
