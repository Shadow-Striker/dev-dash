using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownwardsMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed += 0.13f * Time.deltaTime;
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }
}
