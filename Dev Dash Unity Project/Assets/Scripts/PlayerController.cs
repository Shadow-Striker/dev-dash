using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDamagable
{
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    [SerializeField] private int startingHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float deadZone;
    [SerializeField] private int currentLaneNumber = 1;
    [SerializeField] private bool tap, swipeLeft, swipeRight;
    private bool isDragging = false;
    [SerializeField] private bool moveLeft = false;
    [SerializeField] private bool moveRight = false;
    private float distanceBtwnLanes = 2f;
    [SerializeField] private Vector2 startMousePos, endMousePos;
    private Vector2 startingPos;
    [SerializeField] private Vector2[] lanePositions;
    public Vector3 mousePos;

    //IDamagable
    public int StartingHealth
    {
        get
        {
            return startingHealth;
        }
        private set
        {
            startingHealth = value;
        }
    }

    public int Health { get; set; }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        Health = startingHealth;
    }

    private void Update()
    {
        swipeLeft = swipeRight = false;
        MouseCode();
        SwitchLanes();
        if(!gameManager.IsGameOver) LaneMovement();
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
            endMousePos = Input.mousePosition;

            Vector2 moveDirection = (endMousePos - startMousePos).normalized;

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
        if (moveLeft || moveRight) return;

        if (swipeLeft)
        {
            if (currentLaneNumber != 0)
            {
                startingPos = transform.position;
                moveLeft = true;
            }
        }

        else if (swipeRight)
        {
            if (currentLaneNumber != 2)
            {
                startingPos = transform.position;
                moveRight = true;
            }
        }
    }

    private void LaneMovement()
    {
        if (moveLeft)
        {
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            if (transform.position.x <= lanePositions[currentLaneNumber - 1].x)
            {
                transform.position = new Vector2(lanePositions[currentLaneNumber - 1].x, transform.position.y);
                currentLaneNumber -= 1;
                moveLeft = false;
            }
        }

        if (moveRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            if (transform.position.x >= lanePositions[currentLaneNumber + 1].x)
            {
                transform.position = new Vector2(lanePositions[currentLaneNumber + 1].x,transform.position.y);
                currentLaneNumber += 1;
                moveRight = false;
            }
        }
    }

    /*private void Fade()
    {
        float alpha = spriteRenderer.color.a;
        if (spriteRenderer.color.a == 1f)
            alpha -= 1;
        spriteRenderer = alpha;
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "AICar")
        {
            print("Collided with AI Car");
        }
    }

    public void TakeDamage(int _damage)
    {
        if(Health > 0)
        Health -= _damage;
    }
}
