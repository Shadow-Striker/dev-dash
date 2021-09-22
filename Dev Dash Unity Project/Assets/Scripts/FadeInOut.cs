using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut
{
    //This script let's you
    //Other classes will decide when the fade in/out are called.
    //Fade in and/or fade out
    //Modify fade in time and fade out time seperately
    //Apply to any gameObject
    //Reference to spriteRenderer
    //Reference to image (if it's a UI element)
    //Fade in -> fade out

    [SerializeField] private bool canFadeIn;
    [SerializeField] private bool canFadeOut;
    [SerializeField] private float timeElapsed;
    [SerializeField] private float fadeInDuration;
    [SerializeField] private float fadeOutDuration;
    [SerializeField] private float maxAlpha;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Image image;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FadeIn(SpriteRenderer _spriteRenderer)
    {
        if (_spriteRenderer.color.a < maxAlpha)
        {
            Color tempColour = _spriteRenderer.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(0f, maxAlpha, timeElapsed / fadeInDuration);
        }
        else
        {
            canFadeIn = false;
            timeElapsed = 0f;
        }
    }

    private void FadeIn(Image _image)
    {
        if (_image.color.a < maxAlpha)
        {
            Color tempColour = _image.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(0f, maxAlpha, timeElapsed / fadeInDuration);
        }
        else
        {
            canFadeIn = false;
            timeElapsed = 0f;
        }
    }



    private void FadeOut(SpriteRenderer _spriteRenderer)
    {
        if (_spriteRenderer.color.a >= maxAlpha)
        {
            Color tempColour = _spriteRenderer.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(maxAlpha, 0f, timeElapsed / fadeOutDuration);
        }
        else
        {
            canFadeOut = false;
            timeElapsed = 0f;
        }
    }

    private void FadeOut(Image _image)
    {
        if (_image.color.a >= maxAlpha)
        {
            Color tempColour = _image.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(maxAlpha, 0f, timeElapsed / fadeOutDuration);
        }
        else
        {
            canFadeOut = false;
            timeElapsed = 0f;
        }
    }
}


