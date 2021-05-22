using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector3 direction;
    private SpriteRenderer spriteRenderer;
    private Vector3 screenVector;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Vector3 screenVector = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height));
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.position += speed * direction.normalized * Time.deltaTime;

        if (transform.position.y - spriteRenderer.bounds.extents.y <= -screenVector.y)
        {
            gameObject.SetActive(false);
        }
    }
}
