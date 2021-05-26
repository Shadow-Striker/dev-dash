using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool tap, swipeLeft, swipeRight;
    private bool isDragging = false;
    private Vector2 startTouchPos, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }

    private void Update()
    {
        tap = swipeLeft = swipeRight = false;

        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDragging = true;
            startTouchPos = Input.mousePosition;
        }
        else if(Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            ResetTouchPositions();
        }

        //Calculate distance
        swipeDelta = Vector3.zero;

        if (isDragging)
        {
            if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouchPos;
            }
            else if(Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                ResetTouchPositions();
            }
        }

        //Deadzone
        if(swipeDelta.magnitude > 125)
        {
            //Calculate direction of swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (x < 0)
            {
                swipeLeft = true;
            }
            else
            {
                swipeRight = true;
            }

            ResetTouchPositions();
        }

        Movement();
    }

    private void ResetTouchPositions()
    {
        startTouchPos = Vector2.zero;
        swipeDelta = Vector2.zero;
    }

    private void Movement()
    {
        if (swipeLeft)
        {
            //transform.position += Vector3.left * speed;
            print("Move left");
        }
        else if (swipeRight)
        {
            //transform.position += Vector3.right * speed;
            print("Move right");
        }
    }
}
