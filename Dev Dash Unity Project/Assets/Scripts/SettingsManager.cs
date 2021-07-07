using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    private static SettingsManager instance;
    private static bool screenShake;
    
    public bool ScreenShake
    {
        get
        {
            return screenShake;
        }
        private set
        {

        }

    }


    private void Awake()
    {
        //Checks if a music player already exists.
        //If it does destroy so there are not multiple music players.
        if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void ToggleScreenShake(bool _screenShake)
    {
        screenShake = _screenShake;
    }

}
