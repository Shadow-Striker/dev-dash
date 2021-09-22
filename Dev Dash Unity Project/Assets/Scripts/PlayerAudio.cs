using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioSource dashSFXAudioSource;
    private float randFloat;

    private void Awake()
    {
        PlayerController.OnSwitchLane += PlayLaneSwitchAudio;
    }

    private void OnDisable()
    {
        PlayerController.OnSwitchLane -= PlayLaneSwitchAudio;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayLaneSwitchAudio()
    {
        randFloat = Random.Range(0.8f, 1.1f);
        dashSFXAudioSource.pitch = randFloat;
        dashSFXAudioSource.Play();
    }
}
