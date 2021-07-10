using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SettingsManager : MonoBehaviour
{
    private static SettingsManager instance = null;
    [SerializeField] private int id;
    [SerializeField] private Toggle screenShakeToggle;
    public static SettingsManager Instance
    {
        get
        {
            return instance;
        }
    }
    
    [SerializeField] private bool screenShake;
    
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
        screenShakeToggle = FindObjectOfType<Toggle>();

        id = gameObject.GetInstanceID();
        /* if (instance != null && instance != this)
         {
             print("Destroy settingsManager (newly created)" + gameObject.GetInstanceID());
             print("Instance ID: + " + instance.gameObject.GetInstanceID());
             Destroy(gameObject);
             return;
         }
         else if(instance == null)
         {
             instance = this;
             DontDestroyOnLoad(gameObject);
         }*/
         if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            this.screenShake = instance.screenShake;
            Destroy(instance.gameObject);
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }


    private void OnLevelWasLoaded(int level)
    {
        if(level == 1)
        screenShakeToggle = FindObjectOfType<Toggle>();
        screenShakeToggle.onValueChanged.AddListener(delegate { ToggleScreenShake(screenShakeToggle.isOn); });
        // screenShakeToggle.
        screenShakeToggle.isOn = screenShake;
    }


    public void ToggleScreenShake(bool _screenShake)
    {
        screenShake = _screenShake;
    }

}
