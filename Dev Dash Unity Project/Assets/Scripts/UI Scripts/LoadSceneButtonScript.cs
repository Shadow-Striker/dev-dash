using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneButtonScript : MonoBehaviour
{
    [SerializeField] private GameObject creditsScreen;
    [SerializeField] private GameObject menuScreen;
    [SerializeField] private GameObject socialScreen;
    [SerializeField] private float targetX;
    private float velocity = 0.0f;
    [SerializeField] private float smoothTime = 0.3f;
    //Loads game scene when start button is clicked.
    public void LoadGameScene(int _buildIndex)
    {
        SceneManager.LoadScene(_buildIndex);
    }

    public void ToggleCreditsScreen()
    {
        bool creditsBool = creditsScreen.activeSelf;
        bool menuBool = menuScreen.activeSelf;

        creditsScreen.SetActive(!creditsBool);
        menuScreen.SetActive(!menuBool);
    }

    public void ToggleSocialScreen()
    {
        bool socialBool = socialScreen.activeSelf;
        bool menuBool = menuScreen.activeSelf;

        socialScreen.SetActive(!socialBool);
        menuScreen.SetActive(!menuBool);
    }
}
