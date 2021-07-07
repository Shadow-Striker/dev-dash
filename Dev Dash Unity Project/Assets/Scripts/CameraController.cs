using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Animator camAnim;
    [SerializeField] private float leftPosMax;
    [SerializeField] private float rightPosMax;
    [SerializeField] private float moveSpeed;
    private SettingsManager settingsManager;
    public bool camMoveLeft = false;
    public bool camMoveRight = false;
    // Start is called before the first frame update
    void Start()
    {
        settingsManager = FindObjectOfType<SettingsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CamShake()
    {
        if(settingsManager != null && settingsManager.ScreenShake)
        {
            camAnim.SetTrigger("shake");
        }
    }

    public void MoveLeft()
    {
        print("move camera left");
       // float movePosX = transform.position.x + leftPosMax;

        transform.position += moveSpeed * Vector3.left * Time.deltaTime;

        /*if(transform.position.x <= movePosX)
        {
            transform.position = new Vector3(leftPosMax,transform.position.y,transform.position.z);
        }*/
    }

    public void MoveRight()
    {
        print("move camera right");
        transform.position += moveSpeed * Vector3.right * Time.deltaTime;

        /*if (transform.position.x >= movePosX)
        {
            transform.position = new Vector3(rightPosMax, transform.position.y, transform.position.z);
        }*/
    }
}
