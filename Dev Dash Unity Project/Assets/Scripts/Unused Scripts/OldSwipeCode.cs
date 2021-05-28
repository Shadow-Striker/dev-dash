using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// NO LONGER BEING USED
/// FOR REFERENCE PURPOSES ONLY
/// REFER TO PLAYERCONTROLLER FOR NEW SWIPING CODE
/// </summary>
public class OldSwipeCode : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float deadZone;
    [SerializeField] private bool tap, swipeLeft, swipeRight;
    private bool isDragging = false;
    [SerializeField] private Vector2 startTouchPos, swipeDelta;
    public Vector3 mousePos;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }

    private void Update()
    {
        mousePos = Input.mousePosition;


        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouchPos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            ResetTouchPositions();
        }
        tap = false;

        //Calculate distance
        swipeDelta = Vector3.zero;

        if (isDragging)
        {
            if (Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouchPos;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
                ResetTouchPositions();
            }
        }

        //Deadzone
        if (swipeDelta.magnitude > deadZone)
        {
            //Calculate direction of swipe
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (x < 0)
            {
                swipeLeft = true;
            }
            else if (x > 0)
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
            transform.position = new Vector3(-2, transform.position.y, 0);
            print("Move left");
            swipeLeft = false;
        }
        else if (swipeRight)
        {
            transform.position = new Vector3(2, transform.position.y, 0);
            print("Move right");
            swipeRight = false;
        }
    }
}
