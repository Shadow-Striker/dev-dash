using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerControllerTwo : MonoBehaviour, IDamagable
{
    private SpriteRenderer spriteRenderer;
    private PlayerInput playerInput;
    private GameManager gameManager;
    private ScreenFlash screenFlash;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool damageImmunity;
    [SerializeField] private float damageImmuneTime;
    [SerializeField] private float damageImmuneTimeLeft;
    [SerializeField] private int startingHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float deadZone;
    [SerializeField] private int currentLaneNumber = 1;
    [SerializeField] private bool tap;
    [SerializeField] private bool freezeFrames = false;
    [SerializeField] private int frameCount = 0;
    private bool isDragging = false;
    [SerializeField] private bool moveLeft = false;
    [SerializeField] private bool moveRight = false;
    private float distanceBtwnLanes = 2f;
    [SerializeField] private Vector2 startMousePos, endMousePos;
    private Vector2 startingPos;
    [SerializeField] private Vector2[] lanePositions;
    [SerializeField] private bool decreaseAlpha = false;
    [SerializeField] private bool increaseAlpha = false;
    private static float timeElapsed = 0.0f;
    [SerializeField] private float alphaLerpDuration = 2f;

    [SerializeField] private ParticleSystem dashParticles, burstParticles;
    private bool playDashSound = false;
    [SerializeField] private float switchLaneDelayLeft;
    [SerializeField] private float switchLaneDelay;
    [SerializeField] private bool startMoveDelay = false;
    [SerializeField] private float flashTimeLeft;
    [SerializeField] private float flashTime;
    [SerializeField] private bool startFlash = false;
    private Color startingColour;

    public delegate void ActionSwitchLane();
    public static event ActionSwitchLane OnSwitchLane;

    public delegate void ActionTakeDamage();
    public static event ActionTakeDamage OnTakeDamage;

    public delegate void ActionPauseButton();
    public static event ActionPauseButton OnPressPauseButton;


    //These variables are from the IDamagable interface.
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

    public bool DamageImmunity
    {
        get
        {
            return damageImmunity;
        }

        private set
        {
            damageImmunity = value;
        }
    }


    public int Health { get; set; }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
        gameManager = FindObjectOfType<GameManager>();
        screenFlash = FindObjectOfType<ScreenFlash>();
        Health = startingHealth;
        damageImmuneTimeLeft = damageImmuneTime;
        startingColour = spriteRenderer.color;
        flashTimeLeft = flashTime;
    }

    private void Update()
    {
        if (gameManager.IsGameOver == true || gameManager.HasWonGame == true) return;
        SwitchLanes();

        if (damageImmunity)
        {
            ImmuneAnimCheck();
        }
        else
        {
            Color tempColour = spriteRenderer.color;
            tempColour.a = 1f;
            spriteRenderer.color = tempColour;
        }


        if (gameManager.IsGameOver == false || gameManager.HasWonGame == false)
        {
            LaneMovement();
        }

        if (freezeFrames) FreezeFraming();

        if (startMoveDelay)
        {
            switchLaneDelayLeft -= Time.deltaTime;
        }
        if (switchLaneDelayLeft <= 0)
        {
            switchLaneDelayLeft = switchLaneDelay;
            startMoveDelay = false;
        }

        if (SwitchLanes() == moveLeft)
        {
            MoveToLeftLane();
        }
        else if(SwitchLanes() == moveRight)
        {
            MoveToRightLane();
        }

        //if the player is currently immune to damage & damageImmuneTimeLeft is > 0,
        //Decrease damageImmuneTimeLeft by 1 every frame
        //Else reset damageImmuneTimeLeft and set damageImmunity to false.
        DamageImmunityTimer();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            OnPressPauseButton?.Invoke();
        }
    }

    //Prepare to switch lanes
    private bool SwitchLanes()
    {
        //Don't move the player to another lane if they are currently doing so.
        if (moveLeft || moveRight) return false;

        //Check if the player is not on the left most lane before moving the player to the left.
        //This is to ensure the player does not go offscreen.
        if (playerInput.InputLeft && currentLaneNumber != 0)
        {
            dashParticles.Play();
            OnSwitchLane?.Invoke();
            startingPos = transform.position;
            playDashSound = true;
            return moveLeft = true;
        }

        //Same as above but for swiping right instead.
        else if (playerInput.InputRight && currentLaneNumber != 2)
        {
            dashParticles.Play();
            OnSwitchLane?.Invoke();
            startingPos = transform.position;
            return moveRight = true;
        }

        return false;
    }

    //Actually switches between the lanes.
    private void LaneMovement()
    {
        //Don't execute this function if the player has won/lost.
        if (gameManager.IsGameOver == true || gameManager.HasWonGame == true || startMoveDelay) return;

        if (moveLeft)
        {

        }


        //Same as above but for moving right instead.
        if (moveRight)
        {

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player collides with AI Car, set damageImmunity to true.
        if (collision.gameObject.tag == "AICar" && !damageImmunity)
        {
            startFlash = true;
            damageImmunity = true;
        }
    }

    //Function from IDamagable interface.
    public void TakeDamage(int _damage)
    {
        if (Health > 0)
        {
            Health -= _damage;
            OnTakeDamage?.Invoke();
        }

        if (!screenFlash.startScreenFlash)
        {
            screenFlash.startScreenFlash = true;
        }
    }

    /* FOR INT I = 0 INT I< 6 I++
     IF spriteRenderer.color.a == 1
     THEN spriteRender.color.a = Mathf.Lerp(1, 0, 2)
     ELSE IF spriteRenderer.color.a == 0
     THEN spriteRender.color.a = Mathf.Lerp(1, 0, 2)
     END IF
     END FOR*/


    private void ImmuneAnimCheck()
    {
        if (spriteRenderer.color.a == 1f)
        {
            decreaseAlpha = true;
        }

        if (decreaseAlpha && ImmuneAnim(1f, .4f).a <= 0.4f)
        {
            timeElapsed = 0.0f;
            decreaseAlpha = false;
        }
        else if (ImmuneAnim(.4f, 1f).a == 1)
        {
            timeElapsed = 0.0f;
            return;
        }
    }

    private Color ImmuneAnim(float _startAlphaValue, float _endAlphaValue)
    {
        Color tempColour = spriteRenderer.color;
        timeElapsed += Time.deltaTime;
        tempColour.a = Mathf.Lerp(_startAlphaValue, _endAlphaValue, timeElapsed / alphaLerpDuration);
        spriteRenderer.color = tempColour;
        return tempColour;
    }

    private void FreezeFraming()
    {
        if (freezeFrames)
        {
            if (frameCount < 10)
            {
                Time.timeScale = 0f;
                frameCount += 1;
            }
            else
            {
                Time.timeScale = 1f;
                frameCount = 0;
                freezeFrames = false;
            }
        }
    }

    private void HitFlash()
    {
        spriteRenderer.sprite = sprites[1];
        spriteRenderer.color = Color.white;
        flashTimeLeft -= Time.deltaTime;

        if (flashTimeLeft <= 0)
        {
            spriteRenderer.color = startingColour;
            spriteRenderer.sprite = sprites[0];
            flashTimeLeft = flashTime;
            timeElapsed = 0f;
            startFlash = false;
        }
    }

    private void DamageImmunityTimer()
    {
        if (!damageImmunity) return;

        if (damageImmuneTimeLeft > 0)
        {
            damageImmuneTimeLeft -= 1 * Time.deltaTime;
        }
        else
        {
            damageImmuneTimeLeft = damageImmuneTime;
            damageImmunity = false;
        }
    }

    private void MoveToLeftLane()
    {
        //Move player to the left.
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        //If the player's position is <= to the lane that they were supposed to be moving to,
        //i.e if the player has gone further than they need to, reset the player's position.
        if (transform.position.x <= lanePositions[currentLaneNumber - 1].x)
        {
            burstParticles.transform.position -= new Vector3(.5f, 0f, 0f);
            burstParticles.Play();
            transform.position = new Vector2(lanePositions[currentLaneNumber - 1].x, transform.position.y);
            currentLaneNumber -= 1;
            moveLeft = false;
            startMoveDelay = true;
        }
    }

    private void MoveToRightLane()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        if (transform.position.x >= lanePositions[currentLaneNumber + 1].x)
        {
            burstParticles.transform.position += new Vector3(.5f, 0f, 0f);
            burstParticles.Play();
            transform.position = new Vector2(lanePositions[currentLaneNumber + 1].x, transform.position.y);
            currentLaneNumber += 1;
            moveRight = false;
            startMoveDelay = true;
        }
    }
}

