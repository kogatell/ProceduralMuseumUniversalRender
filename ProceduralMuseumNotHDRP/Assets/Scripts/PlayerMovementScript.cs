using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    #region Public variables
    public float speed = 2.0f;
    public float verticalSpeed = 2.0f;
    public float rotationSpeed = 2.0f;
    public float decelerationValue = 2.0f;
    #endregion

    #region Private variables
    private float zAxisInput;
    private float xAxisInput;
    private bool spacePressed;
    private bool shiftPressed;
    private bool xPressed;
    private int verticalMultiplicationValue;
    private Vector3 headCoords;
    private GameObject head;
    private Rigidbody rb;
    private enum stateEnum
    {
        moving,
        movinghead
    }
    private stateEnum state;
    #endregion

    void Start()
    {
        state = stateEnum.moving;
        rb = this.GetComponent<Rigidbody>();
        head = GameObject.FindGameObjectWithTag("Head");
    }

    void Update()
    {
        InputChecks();
        switch (state)
        {
            case stateEnum.moving:
                Move();
                //Debug.Log("Moving");
                break;
            case stateEnum.movinghead:
                MoveHead();
                //Debug.Log("Moving head");
                break;
        }
    }
    
    void Move()
    {
        
        Vector3 movement = new Vector3(xAxisInput * speed, 0, 0);
        transform.Rotate(0, zAxisInput * 360 * Time.deltaTime, 0);
        if (xAxisInput != 0)
        {
            rb.velocity = transform.forward * speed * xAxisInput;
        }
        else
        {
            rb.velocity = rb.velocity * decelerationValue * Time.deltaTime;
        }
        

        //transform.forward = new Vector3(xAxisInput * speed * Time.deltaTime +transform.forward.x, transform.forward.y, transform.forward.z);
        //rb.AddForce(movement);
        if (xPressed)
        {
            state = stateEnum.movinghead;
        }
    }
    void MoveHead()
    {
        //head.transform.position = new Vector3 (head.transform.position.x + speed * Time.deltaTime * xAxisInput, head.transform.position.y + verticalSpeed * Time.deltaTime * )
        
        if (xPressed)
        {
            state = stateEnum.moving;
        }
    }

    void InputChecks()
    {
        zAxisInput = Input.GetAxisRaw("Horizontal");
        xAxisInput = Input.GetAxisRaw("Vertical");
        spacePressed = Input.GetKeyDown(KeyCode.Space);
        shiftPressed = Input.GetKeyDown(KeyCode.LeftShift);
        xPressed = Input.GetKeyDown(KeyCode.X);
        if (spacePressed)
        {
            verticalMultiplicationValue = 1;
        }
        else if(shiftPressed)
        {
            verticalMultiplicationValue = -1;
        }
        else
        {
            verticalMultiplicationValue = 0;
        }
    }
}
