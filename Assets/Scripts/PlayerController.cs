// Name: PlayerController.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Controls how the player moves

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public & Serialized Variables
    [Header("Movement")]
    [SerializeField] float moveSpeed = 1f;              // How fast the player moved
    [SerializeField] float jumpForce = 1f;              // How high the player jumps
    [SerializeField] float floatForce = 1f;             // How much force is applied with the flap action
    [SerializeField] float groundCheckRadius = 0.5f;    // 
    [SerializeField] float slopeCheckDistance = 0.5f;   // 

    [Header("Physics Materials")]
    [SerializeField] PhysicsMaterial2D noFriction;      // Physics material for platforms with no friction
    [SerializeField] PhysicsMaterial2D fullFriction;    // Physics material for platforms with full friction

    [Header("Ground Checks")]
    [SerializeField] Transform groundCheck; //
    [SerializeField] LayerMask groundLayer; // 

    public enum ControlScheme {JUMP, FLAP, GRAVITY, DASH, NONE};   // Enum types for all possible control schemes the player has
    public ControlScheme controlScheme;                             // Reference to the currently used control scheme
    #endregion

    #region Private Variables
    private Rigidbody2D rb;         // Reference to the player's Rigidbody2D
    private BoxCollider2D bc;       // Reference to the player's BoxCollider2D

    private Vector2 colliderSize;       // Reference to the size of the player's CapsuleCollider2D
    private Vector2 slopeNormalPerp;    // 
    private Vector2 newVelocity;        // 

    private bool isOnSlope;         // If the player is on a slope or not
    private bool isJumping;         // If the player is jumping or not
    private bool canJump;           // If the player is able to jump or not
    private bool isGrounded;        // If the player is grounded or not
    private bool canWalkOnSlope;    // If the player can walk on a slope or not

    private float moveX;                // Stores the horizontal input of the player
    private float maxSlopeAngle;        // Maximum angle the player can jump from
    private float slopeDownAngle;       // 
    private float slopeDownAngleOld;    // 
    private float slopeSideAngle;       // 
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       // Grab the Rigidbody2D attached to the player
        bc = GetComponent<BoxCollider2D>();     // Grab the CapsuleCollider2D attached to the player
        colliderSize = bc.size;                 // Grab the size of the CapsuleCollider2D

        controlScheme = ControlScheme.JUMP;    // Default control scheme to BASIC
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();   // Check the player input
    }

    // Fixed Update
    void FixedUpdate()
    {
        CheckGround();
        SlopeCheck();
        ApplyMovement();
    }

    // Check the input of the player
    private void CheckInput()
    {
        // Movement
        moveX = Input.GetAxis("Horizontal");

        // Action
        if (Input.GetKeyDown(KeyCode.Space))    // When the ACTION key is pressed...
        {
            if (controlScheme == ControlScheme.JUMP)   // If the control scheme is set to BASIC...
            {
                if (canJump)    // If the player can currently jump...
                {
                    canJump = false;
                    isJumping = true;

                    newVelocity.Set(0.0f, 0.0f);
                    rb.velocity = newVelocity;

                    rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);
                }
            }

            else if (controlScheme == ControlScheme.FLAP)   // If the control scheme is set to FLAP...
            {
                isJumping = true;

                newVelocity.Set(0.0f, 0.0f);
                rb.velocity = newVelocity;

                rb.AddForce(new Vector2(0, floatForce), ForceMode2D.Impulse);    // Add an upwards force to the player's Rigidbody
            }

            else if (controlScheme == ControlScheme.GRAVITY)    // If the control scheme is set to GRAVITY...
            {
                isJumping = true;

                newVelocity.Set(0.0f, 0.0f);
                rb.velocity = newVelocity;

                rb.AddForce(new Vector2(0.0f, jumpForce), ForceMode2D.Impulse);

                if (rb.gravityScale == 1)
                {
                    rb.gravityScale = -1;
                    rb.AddForce(new Vector2(0, floatForce), ForceMode2D.Impulse);
                }
                else
                {
                    rb.gravityScale = 1;
                    rb.AddForce(new Vector2(0, -floatForce), ForceMode2D.Impulse);
                }
            }
            
            else if (controlScheme == ControlScheme.DASH)   // If the control scheme is set to DASH...
            {
                newVelocity.Set(0.0f, 0.0f);
                rb.velocity = newVelocity;

                rb.AddForce(new Vector2(floatForce, 0), ForceMode2D.Force);
            }
        }

        // DEBUG Switch State
        if (Input.GetKey(KeyCode.Alpha1))   // Switch to BASIC
        {
            controlScheme = ControlScheme.JUMP;
            Debug.Log("Set control scheme to " + controlScheme);
        }

        if (Input.GetKey(KeyCode.Alpha2))   // Switch to FLAP
        {
            controlScheme = ControlScheme.FLAP;
            Debug.Log("Set control scheme to " + controlScheme);
        }

        if (Input.GetKey(KeyCode.Alpha3))   // Switch to GRAVITY
        {
            controlScheme = ControlScheme.GRAVITY;
            Debug.Log("Set control scheme to " + controlScheme);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))   // Switch to DASH
        {
            controlScheme = ControlScheme.DASH;
            Debug.Log("Set control scheme to " + controlScheme);
        }
    }

    // Check Ground
    private void CheckGround()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer); // Check to see if the player is grounded, then set the bool

        if (rb.velocity.y <= 0.0f)
        {
            isJumping = false;
        }

        if (isGrounded && !isJumping)
        {
            canJump = true;
        }
    }

    // Slope Check
    private void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colliderSize.y / 2, 0.0f);

        SlopeCheckX(checkPos);
        SlopeCheckY(checkPos);
    }

    private void SlopeCheckX(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, groundLayer);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, groundLayer);

        if (slopeHitFront)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }

        else if (slopeHitBack)
        {
            isOnSlope = true;
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }

        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }

    private void SlopeCheckY(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, groundLayer);

        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;

            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (slopeDownAngle != slopeDownAngleOld)
            {
                isOnSlope = true;   // Player is on a slope
            }

            slopeDownAngleOld = slopeDownAngle;

            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
            Debug.DrawRay(hit.point, hit.normal, Color.green);
        }

        if (isOnSlope && moveX == 0.0f)
        {
            rb.sharedMaterial = fullFriction;
        }

        else
        {
            rb.sharedMaterial = noFriction;
        }
    }

    // Apply Movement
    private void ApplyMovement()
    {
        // If not on a slope...
        if (isGrounded && !isOnSlope && !isJumping)
        {
            newVelocity.Set(moveSpeed * moveX, 0.0f);
            rb.velocity = newVelocity;
        }

        // If on a slope...
        else if (isGrounded && isOnSlope && !isJumping)
        {
            newVelocity.Set(moveSpeed * slopeNormalPerp.x * -moveX, moveSpeed * slopeNormalPerp.y * -moveX);
            rb.velocity = newVelocity;
        }

        // If in the air...
        else if (!isGrounded)
        {
            newVelocity.Set(moveSpeed * moveX, rb.velocity.y);
            rb.velocity = newVelocity;
        }
    }
    #endregion
}