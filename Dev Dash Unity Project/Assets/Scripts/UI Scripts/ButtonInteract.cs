using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonInteract : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    private float timeElapsed;
    private AudioSource audioSource;
    [SerializeField] private float scaleLerpDuration = .05f;
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private bool startEnlargeAnim = false;
    [SerializeField] private bool startShrinkAnim = false;
    private Vector3 startingScale;
    private enum States { idleState, shrinkState, enlargeState };
    [SerializeField] private States currentState;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.Find("UI Click SFX").GetComponent<AudioSource>();
        startingScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // if(startEnlargeAnim && !startShrinkAnim)
        //  {
        //      PlayEnlargeAnim();
        // }

        switch (currentState)
        {
            case States.idleState:
                break;
            case States.shrinkState:
                PlayShrinkAnim();
                break;
            case States.enlargeState:
                PlayEnlargeAnim();
                break;

        }



        // if (startShrinkAnim && !startEnlargeAnim)
        //  {
        //    PlayShrinkAnim();
        // }
    }

    private void PlayEnlargeAnim()
    {
        timeElapsed += Time.deltaTime;
        transform.localScale = Vector3.Lerp(startingScale, startingScale * scaleMultiplier, timeElapsed / scaleLerpDuration);

        if (transform.localScale.x >= startingScale.x * scaleMultiplier)
        {
            timeElapsed = 0f;
            currentState = States.idleState;
            startEnlargeAnim = false;
        }

    }

    private void PlayShrinkAnim()
    {
        timeElapsed += Time.deltaTime;
        transform.localScale = Vector3.Lerp(startingScale * scaleMultiplier, startingScale, timeElapsed / scaleLerpDuration);

        if (transform.localScale.x <= startingScale.x)
        {
            timeElapsed = 0f;
            currentState = States.idleState;
            startShrinkAnim = false;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        startEnlargeAnim = true;
        currentState = States.enlargeState;
        audioSource.pitch = Random.Range(0.8f, 1.1f);
        audioSource.Play();
        print("Hovering over button");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        currentState = States.shrinkState;
        startShrinkAnim = true;
        print("Exited button");
    }

    /*public void OnPointerClick(PointerEventData pointerEventData)
    {
        if(pointerEventData.button == PointerEventData.InputButton.Left)
        {
            print("Button clicked");
            currentState = States.shrinkState;
        }
    }*/

    //OnPointerDown is also required to receive OnPointerUp callbacks
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            print("Button held down");
            currentState = States.shrinkState;
        }
    }

    //Do this when the mouse click on this selectable UI object is released.
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        if (pointerEventData.button == PointerEventData.InputButton.Left)
        {
            currentState = States.idleState;
        }
    }
}
