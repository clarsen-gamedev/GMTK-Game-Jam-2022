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
    [SerializeField] float moveSpeed = 1f;  // How fast the player moved
    [SerializeField] float jumpForce = 1f;  // How high the player jumps

    public enum ControlScheme {NONE, BASIC, FLAP, GRAVITY}; // Enum types for all possible control schemes the player has
    public ControlScheme controlScheme;                     // Reference to the currently used control scheme
    #endregion

    #region Private Variables
    private Rigidbody2D rigidbody;  // Reference to the player's Rigidbody2D
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();    // Grab the Rigidbody2D attached to the player
        controlScheme = ControlScheme.BASIC;        // Default control scheme to BASIC
    }

    // Update is called once per frame
    void Update()
    {
        // Moving left and right
        float moveX = Input.GetAxis("Horizontal");                                  // Stores the direction when the left or right input is pressed
        Vector3 movement = new Vector3(moveX, 0, 0) * Time.deltaTime * moveSpeed;   // Create a movement vector to add to the player's position
        transform.position += movement;                                             // Move the player

        // Action
        if (Input.GetKeyDown(KeyCode.Space))    // When the ACTION key is pressed...
        {
            if (controlScheme == ControlScheme.BASIC)   // If the control scheme is set to BASIC...
            {
                rigidbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse); // Add an upwards force to the player's Rigidbody
            }
        }

        // DEBUG Switch State
        if (Input.GetKeyDown(KeyCode.Q))
        {
            controlScheme = controlScheme++;
            Debug.Log("Set control scheme to " + controlScheme);
        }
    }
    #endregion
}