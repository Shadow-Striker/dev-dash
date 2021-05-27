using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float deadZone;
    [SerializeField] private int currentLaneNumber = 1;
    [SerializeField] private bool tap, swipeLeft, swipeRight;
    private bool isDragging = false;
    private bool moveLeft = false;
    private bool moveRight = false;
    private float distanceBtwnLanes = 2f;
    [SerializeField] private Vector2 startMousePos, endMousePos;
    [SerializeField] private Vector2[] lanePositions;
    public Vector3 mousePos;

    //public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }

    private void Start()
    {
        
    }

    private void Update()
    {
        swipeLeft = swipeRight = false;
        MouseCode();
        SwitchLanes();
    }

    private void MouseCode()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("Held down mouse button");
            startMousePos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            //float moveLength = (endMousePos - startMousePos).sqrMagnitude;

           // if (moveLength < 30) return;

            endMousePos = Input.mousePosition;

            Vector2 moveDirection = (endMousePos - startMousePos).normalized;
            print("Move Dir: " + moveDirection);


            if(moveDirection.x < 0)
            {
                swipeLeft = true;
            }
            else if(moveDirection.x > 0)
            {
                swipeRight = true;
            }
        }


    }

    private void SwitchLanes()
    {
        if (swipeLeft)
        {
            if (transform.position.x != lanePositions[0].x)
            {
                moveLeft = true;
               // transform.position = new Vector3(lanePositions[currentLaneNumber - 1].x, transform.position.y, 0);
                currentLaneNumber -= 1;
            }
        }

        else if (swipeRight)
        {
            if (transform.position.x != lanePositions[2].x)
            {
                transform.position = new Vector3(lanePositions[currentLaneNumber + 1].x, transform.position.y, 0);
                currentLaneNumber += 1;
            }
        }

        if(moveLeft && transform.position.x != -distanceBtwnLanes)
        {
            transform.position += Vector3.left * distanceBtwnLanes * Time.deltaTime;
        }
        else
        {

        }

        while(moveLeft)
        {
            transform.position += Vector3.left * distanceBtwnLanes * Time.deltaTime;
            if(transform.position.x <= -distanceBtwnLanes)
            {
                moveLeft = false;
            }
        }

        while (moveRight)
        {
            transform.position += Vector3.right * distanceBtwnLanes * Time.deltaTime;
            if (transform.position.x >= distanceBtwnLanes)
            {
                moveLeft = false;
            }
        }
    }
    
}
