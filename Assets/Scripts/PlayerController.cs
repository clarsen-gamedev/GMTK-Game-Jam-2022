// Name: PlayerController.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Script which controls the player character

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Public & Serialized Variables
    public Rigidbody rb;
    public float moveSpeed = 10f;
    #endregion

    #region Private Variables
    private float xInput;
    private float yInput;
    #endregion

    #region Functions
    // Awake is called before the game loads
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        ProcessInputs();    // Handle player inputs
    }

    // FixedUpdate is called at a consistent rate
    private void FixedUpdate()
    {
        Move(); // Move the player
    }

    // Handle player inputs
    private void ProcessInputs()
    {
        xInput = Input.GetAxis("Horizontal");   // Grab the input on the x-axis
        yInput = Input.GetAxis("Vertical");     // Grab the input on the y-axis
    }

    // Move the player
    private void Move()
    {
        rb.AddForce(new Vector3(xInput, 0f, yInput) * moveSpeed);
    }
    #endregion
}