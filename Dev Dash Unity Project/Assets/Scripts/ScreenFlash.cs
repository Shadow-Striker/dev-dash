using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] private float alphaLerpDuration;
    [SerializeField] private float timeElapsed;
    private Image image;
    private PlayerController playerController;
    [SerializeField] private bool decreaseAlpha;
    [SerializeField] private float maxAlpha;
    [SerializeField] public bool startScreenFlash;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        playerController = FindObjectOfType<PlayerController>();
    }

    private void Update()
    {
        if(startScreenFlash)
        { 
            FlashScreen();
        }
    }

    public void FlashScreen()
    {
      //  if (!startScreenFlash) return;
      //1. go to full alpha
      //2. go to zero alpha

        if (image.color.a < maxAlpha && !decreaseAlpha)
        {
            Color tempColour = image.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(0f, maxAlpha, timeElapsed / alphaLerpDuration);
            image.color = tempColour;

            if(image.color.a >= maxAlpha)
            {
                timeElapsed = 0.0f;
                decreaseAlpha = true;
            }
        }
        else if(decreaseAlpha)
        {
            Color tempColour = image.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(maxAlpha, 0f, timeElapsed / alphaLerpDuration);
            image.color = tempColour;

            if(image.color.a == 0f)
            {
                decreaseAlpha = false;
                startScreenFlash = false;
            }
        }
    }
}
