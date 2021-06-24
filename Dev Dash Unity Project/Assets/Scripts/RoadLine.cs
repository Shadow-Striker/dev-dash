using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLine : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Vector3 direction = Vector3.down;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        moveSpeed += 0.05f * Time.deltaTime;
        transform.position += moveSpeed * direction.normalized * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
