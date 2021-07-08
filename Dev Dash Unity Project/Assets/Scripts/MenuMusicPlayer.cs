using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private static MenuMusicPlayer instance;

    private void Awake()
    {
        //Checks if a music player already exists.
        //If it does destroy so there are not multiple music players.
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }


        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //This allows you to remove the music player after you don't need it.
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            Destroy(gameObject);
        }
    }
}
