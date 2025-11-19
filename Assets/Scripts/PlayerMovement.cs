using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=f473C43s8nE&t=1s

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask whatIsGround; //Restricts player from jumping on things that aren't labelled "whatIsGround"
    bool grounded;

    public float groundDrag;

    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    bool readyToJump;

    public float dashSpeed;

    [Header("Keybinds")]
    public KeyCode jumpKey = KeyCode.Space;

    Vector3 moveDirection; //direction of player to (x,y,z) variable

    Rigidbody rb;

    public MovementState state;

    public enum MovementState
    {
        dashing,
        walking,
        air
    }

    public bool dashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); //Calls component rigid body to variable
        rb.freezeRotation = true; //stops player from falling over

        readyToJump = true;
    }

    private void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround); //sends a raycast below player to check if they are on the ground
        MyInput();
        StateHandler();


        if (state == MovementState.walking)
        {
            rb.linearDamping = groundDrag; //applies drag to playerobj when on the ground
        }
           
        else
        {
            rb.linearDamping = 0; //removes drag when playerobj is in air
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal"); //Calls keys "AD" to variable
        verticalInput = Input.GetAxisRaw("Vertical"); //Calls keys "WS" to variable

        if(Input.GetKey(jumpKey) && readyToJump && grounded) //Checks if player has no jump cooldown, is on the ground, and pressed space
        {
            readyToJump = false; //applies jump cooldown

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown); //Lets the player to hold space and jump constantly
        }
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput; //Calculates player orientation of movement

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force); //Applies calculated speed of player in the direction they input
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force); //Changes player speed in air
        }
        
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse); //Applies a force upward to playerobj
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

    private void StateHandler()
    {
        //Mode - Dashing??????????
        if (dashing)
        {
            state = MovementState.dashing;
            moveSpeed = dashSpeed;
        }

        else if (grounded)
        {
            state = MovementState.walking;
            moveSpeed = walkSpeed;
        }

        else
        {
            state = MovementState.air;
        }

    }
}
