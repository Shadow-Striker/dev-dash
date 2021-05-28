using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //If player hasn't won game yet, move the car.
        //And increase the speed slightly every frame until the speed reaches 7.
        //This is to make the game gradually harder.

        if (!gameManager.HasWonGame)
        {
            Movement();
            if (speed < 7)
                speed += 0.0008f;
        }
    }

    //Moves car downwards every frame.
    void Movement()
    {
        transform.position += speed * direction.normalized * Time.deltaTime;
    }

    //Deactivates car once it is no longer visible by the camera.
    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //When colliding with the player, get their IDamagable interface.
        //If there is an IDamagable interface the player will take damage.

        if (collision.gameObject.tag == "Player")
        {
            IDamagable playerDamagable = collision.gameObject.GetComponent<IDamagable>();

            if(playerDamagable != null && !playerDamagable.DamageImmunity)
            {
                playerDamagable.TakeDamage(1);
            }
        }
    }
}
