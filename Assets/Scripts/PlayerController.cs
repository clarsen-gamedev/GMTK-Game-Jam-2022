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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.rotation = Quaternion.Euler(-90, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.rotation = Quaternion.Euler(90, 0, 0);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            transform.rotation = Quaternion.Euler(0, -90, -90);
        }
    }

    // Move the player
    private void Move()
    {
        rb.AddForce(new Vector3(xInput, 0f, yInput) * moveSpeed);
    }
    #endregion
}