using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITransitions : MonoBehaviour
{
    [SerializeField] private float targetX;
    private float velocity = 0.0f;
    [SerializeField] private float smoothTime = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SlideTransition()
    {
        float newXPos = Mathf.SmoothDamp(transform.position.x, targetX, ref velocity, smoothTime);
        transform.position = new Vector3(newXPos, transform.position.y, transform.position.z);

    }
}
