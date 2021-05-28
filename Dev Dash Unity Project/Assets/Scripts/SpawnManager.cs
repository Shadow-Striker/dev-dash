using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private int timeBetweenSpawns;

    // Start is called before the first frame update
    void Start()
    {
        timeBetweenSpawns = Random.Range(1, 6);
    }
}
