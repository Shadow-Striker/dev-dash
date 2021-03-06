using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IDamagable
{
    private SpriteRenderer spriteRenderer;
    private CameraController cameraController;
    private GameManager gameManager;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private bool damageImmunity;
    [SerializeField] private float damageImmuneTime;
    [SerializeField] private float damageImmuneTimeLeft;
    [SerializeField] private int startingHealth;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float deadZone;
    [SerializeField] private int currentLaneNumber = 1;
    [SerializeField] private bool tap, inputLeft, inputRight;
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
    public Vector3 mousePos;
    [SerializeField] private AudioSource dashSFXAudioSource;
    [SerializeField] private ParticleSystem dashParticles;
    [SerializeField] private ParticleSystem burstParticles;
    [SerializeField] private AudioClip dashSFX;
    private bool playDashSound = false;
    [SerializeField] private float switchLaneDelayLeft;
    [SerializeField] private float switchLaneDelay;
    [SerializeField] private bool startDelay = false;
    [SerializeField] private float flashTimeLeft;
    [SerializeField] private float flashTime;
    [SerializeField] private bool startFlash = false;
    private Color startingColour;
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
    
    //Can be read by other classes but not set.
    public bool SwipeLeft { get { return inputLeft; } }
    public bool SwipeRight { get { return inputRight; } }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        cameraController = FindObjectOfType<CameraController>();
        print(cameraController);
        Health = startingHealth;
        damageImmuneTimeLeft = damageImmuneTime;
        startingColour = spriteRenderer.color;
        flashTimeLeft = flashTime;
    }

    private void Update()
    {
        //Reset swiping left and right to false every frame.
        inputLeft = inputRight = false;
        KeyboardMovement();
        MouseCode();
        SwitchLanes();

        if(damageImmunity)
        {
            PlayImmuneAnim();
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

        if (startDelay)
        {
            switchLaneDelayLeft -= Time.deltaTime;
        }
        if (switchLaneDelayLeft <= 0)
        {
            switchLaneDelayLeft = switchLaneDelay;
            startDelay = false;
        }

        if(startFlash)
        {
            //HitFlash();
        }

        //if the player is currently immune to damage & damageImmuneTimeLeft is > 0,
        //Decrease damageImmuneTimeLeft by 1 every frame
        //Else reset damageImmuneTimeLeft and set damageImmunity to false.
        if (damageImmunity)
        {
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

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            gameManager.TogglePauseState();
        }
    }

    private void KeyboardMovement()
    {
        if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        { 
            inputLeft = true;
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            inputRight = true;
        }
    }

    private void MouseCode()
    {
        //Stores position of the cursor when the left mouse button is clicked.
        if (Input.GetMouseButtonDown(0))
        {
            startMousePos = Input.mousePosition;
        }

        //When the left mouse button is released, store the position of the cursor.
        //These two positions can then be used to calculate the direction of the swipe.
        if(Input.GetMouseButtonUp(0))
        {
            endMousePos = Input.mousePosition;

            Vector2 moveDirection = (endMousePos - startMousePos).normalized;

            //We know the player is swiping left if the direction is negative on the x axis.
            if(moveDirection.x < 0)
            {
                inputLeft = true;
            }
            //We know the player is swiping right if the direction is positive on the x axis.
            else if (moveDirection.x > 0)
            {
                inputRight = true;
            }
        }
    }


    //Prepare to switch lanes
    private void SwitchLanes()
    {
        //Don't move the player to another lane if they are currently doing so.
        if (moveLeft || moveRight) return;

        //Check if the player is not on the left most lane before moving the player to the left.
        //This is to ensure the player does not go offscreen.
        if (inputLeft)
        {
            if (currentLaneNumber != 0)
            {
                dashParticles.Play();
                float randFloat = Random.Range(0.8f, 1.1f);
                dashSFXAudioSource.pitch = randFloat;
                dashSFXAudioSource.Play();
                startingPos = transform.position;
                moveLeft = true;
                playDashSound = true;
            }
        }

        //Same as above but for swiping right instead.
        else if (inputRight)
        {
            if (currentLaneNumber != 2)
            {
                dashParticles.Play();
                float randFloat = Random.Range(.75f, 1.2f);
                dashSFXAudioSource.pitch = randFloat;
                dashSFXAudioSource.Play();
                startingPos = transform.position;
                moveRight = true;
            }
        }
    }

    //Actually switches between the lanes.
    private void LaneMovement()
    {
        //Don't execute this function if the player has won/lost.
        if (gameManager.IsGameOver == true || gameManager.HasWonGame == true || startDelay) return;

        if (moveLeft)
        {
            //Move player to the left.
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;
            cameraController.MoveLeft();
            //If the player's position is <= to the lane that they were supposed to be moving to,
            //i.e if the player has gone further than they need to, reset the player's position.
            if (transform.position.x <= lanePositions[currentLaneNumber - 1].x)
            { 

                burstParticles.transform.position -= new Vector3(.5f, 0f, 0f);
                burstParticles.Play();
                transform.position = new Vector2(lanePositions[currentLaneNumber - 1].x, transform.position.y);
                //cameraController.transform.position = new Vector3(lanePositions[currentLaneNumber - 1].x, cameraController.transform.position.y, cameraController.transform.position.z);
                currentLaneNumber -= 1;
                moveLeft = false;
                startDelay = true;
            }
        }


        //Same as above but for moving right instead.
        if (moveRight)
        {
            transform.position += Vector3.right * moveSpeed * Time.deltaTime;
            cameraController.MoveRight();
            if (transform.position.x >= lanePositions[currentLaneNumber + 1].x)
            {

                burstParticles.transform.position += new Vector3(.5f, 0f, 0f);
                burstParticles.Play();
                transform.position = new Vector2(lanePositions[currentLaneNumber + 1].x,transform.position.y);
                //cameraController.transform.position = new Vector3(lanePositions[currentLaneNumber + 1].x, cameraController.transform.position.y, cameraController.transform.position.z);
                currentLaneNumber += 1;
                moveRight = false;
                startDelay = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player collides with AI Car, set damageImmunity to true.
        if(collision.gameObject.tag == "AICar" && !damageImmunity)
        {
            startFlash = true;
            damageImmunity = true;
            cameraController.CamShake();
            //freezeFrames = true;
            print(collision.gameObject);
        }
    }

    //Function from IDamagable interface.
    public void TakeDamage(int _damage)
    {
        if(Health > 0)
        Health -= _damage;

    }

   /* FOR INT I = 0 INT I< 6 I++
    IF spriteRenderer.color.a == 1
    THEN spriteRender.color.a = Mathf.Lerp(1, 0, 2)
    ELSE IF spriteRenderer.color.a == 0
    THEN spriteRender.color.a = Mathf.Lerp(1, 0, 2)
    END IF
    END FOR*/


    public void PlayImmuneAnim()
    {
       // for (int i = 0; i < 6; i++)
        //{
            if(spriteRenderer.color.a == 1f)
            {
                decreaseAlpha = true;
            }
           // else if(spriteRenderer.color.a == 0f)
           // {
            //    decreaseAlpha = false;
            //}
        //}           

        if(decreaseAlpha)
        {
            Color tempColour = spriteRenderer.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(1f, 0.4f, timeElapsed / alphaLerpDuration);
            spriteRenderer.color = tempColour;

            if(tempColour.a <= 0.4f)
            {
                timeElapsed = 0.0f;
                decreaseAlpha = false;
            }
        }
        else
        {
            Color tempColour = spriteRenderer.color;
            timeElapsed += Time.deltaTime;
            tempColour.a = Mathf.Lerp(0.4f, 1f, timeElapsed / alphaLerpDuration);
            spriteRenderer.color = tempColour;

            if (tempColour.a == 1)
            {
                timeElapsed = 0.0f;
                return;
            }
        }
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
}
