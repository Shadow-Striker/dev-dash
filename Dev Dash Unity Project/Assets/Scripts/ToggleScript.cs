using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleScript : MonoBehaviour
{
    private static ToggleScript instance = null;
    [SerializeField] private int id;
    public static ToggleScript Instance
    {
        get
        {
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        //Checks if a music player already exists.
        //If it does destroy so there are not multiple music players.

        id = gameObject.GetInstanceID();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
