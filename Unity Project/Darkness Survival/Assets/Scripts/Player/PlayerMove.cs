using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMove : MonoBehaviour
{

    [HideInInspector]
    public Vector3 movementVector;

    [HideInInspector]
    public float lastHorizontalVector;

    [HideInInspector]
    public float lastVerticalVector;

    AttackControl attackControl;
    Rigidbody2D rigidbody2d;
    Animator animator;

    [SerializeField] Joystick joystick;

    // Movement Speed 

    [SerializeField] float defaultSpeed = 3.0f;
    float currentSpeed;
    float speed;

    // Look Direction for animations 
    private Vector2 lookDirection = new Vector2(1, 0);

    private void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();

        animator = GetComponent<Animator>();
        attackControl = animator.GetBehaviour<AttackControl>();
        currentSpeed = defaultSpeed;
        speed = defaultSpeed;
    }

    private void Start()
    {
        lastHorizontalVector = 1f;
        lastVerticalVector = 1f;
    }

    /////////////////////////
    // Getters and setters //
    /////////////////////////
    
    public float SPEED
    {
        get { return defaultSpeed; }
        set { currentSpeed = value; }

        // Not card speed ( maybe more useful )
        //get { return currentSpeed; }
    }

    /////////////////////////
    
    // Update is called once per frame
    private void Update()
    {
        if (attackControl.IsAttacking)
        {
            speed = 0f;
            return;
        }
        speed = currentSpeed;

        if (!GameManager.instance.isAndroid)
        {
            movementVector.x = Input.GetAxisRaw("Horizontal");
            movementVector.y = Input.GetAxisRaw("Vertical");
        }
        else
        {

            if (joystick.Horizontal >= .2f)
            {
                movementVector.x = joystick.Horizontal;
            }
            else if (joystick.Horizontal <= -.2f)
            {
                movementVector.x = joystick.Horizontal;
            }
            else
            {
                movementVector.x = 0f;
            }


            if (joystick.Vertical >= .2f)
            {
                movementVector.y = joystick.Vertical;
            }
            else if (joystick.Vertical <= -.2f)
            {
                movementVector.y = joystick.Vertical;
            }
            else
            {
                movementVector.y = 0f;
            }
        }

        movementVector = movementVector.normalized;

        if (movementVector.x != 0f)
        {
            lastHorizontalVector = movementVector.x;
        }
        if (movementVector.y != 0f)
        {
            lastVerticalVector = movementVector.y;
        }

        if (!Mathf.Approximately(movementVector.x, 0.0f))
        {
            lookDirection.Set(movementVector.x, movementVector.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Horizontal", lookDirection.x);
        animator.SetFloat("Speed", movementVector.magnitude);
    }

    private void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x += speed * movementVector.x * Time.deltaTime;
        position.y += speed * movementVector.y * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }
}
