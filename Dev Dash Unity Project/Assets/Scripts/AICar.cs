using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    [SerializeField] public float speed;
    [SerializeField] private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private GameManager gameManager;
    private Vector3 screenVector;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();


        Vector3 screenVector = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.HasWonGame)
        {
            Movement();
            if (speed < 7)
                speed += 0.0008f;
        }
    }

    void Movement()
    {
        transform.position += speed * direction.normalized * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        //if (transform.position.y - spriteRenderer.bounds.extents.y <= -screenVector.y * 2)
       // {
            gameObject.SetActive(false);
       // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
