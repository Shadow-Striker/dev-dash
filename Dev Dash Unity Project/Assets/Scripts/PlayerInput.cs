using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool InputLeft { get; private set; }
    public bool InputRight { get; private set; }

    private Vector3 startMousePos;
    private Vector3 endMousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputLeft = InputRight = false;
        KeyboardInput();
        MouseInput();
    }

    private void KeyboardInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputLeft = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputRight = true;
        }
    }

    private void MouseInput()
    {
        //Stores position of the cursor when the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
        }

        //When the left mouse button is released, store the position of the cursor.
        //These two positions can then be used to calculate the direction of the swipe.
        if (Input.GetMouseButtonUp(0))
        {
            endMousePos = Input.mousePosition;
            Vector2 moveDirection = (endMousePos - startMousePos).normalized;

            if (moveDirection.x == 0) return;

            //We know the player is swiping left if the direction is negative on the x axis.
            if (moveDirection.x < 0)
            {
                InputLeft = true;
            }
            //We know the player is swiping right if the direction is positive on the x axis.
            else
            {
                InputRight = true;
            }
        }
    }
}
