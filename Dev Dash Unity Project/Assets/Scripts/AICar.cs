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
       
        print(screenVector);
        print("Screen Height: " + Screen.height);
        print("Screen Width: " + Screen.width);
    }

    void Movement()
    {
        transform.position += speed * direction.normalized * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        if (transform.position.y - spriteRenderer.bounds.extents.y <= -screenVector.y * 2)
        {
            gameObject.SetActive(false);
        }
    }
}
