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
    public float maxSpeed = 10f;

    // audio dice clips
    public AudioClip dice001;
    public AudioClip dice002;
    public AudioClip dice003;
    public AudioClip dice004;
    public AudioClip dice005;
    public AudioClip dice006;
    public AudioClip dice007;
    public AudioClip dice008;
    public AudioClip dice009;
    public AudioClip dice010;
    public AudioClip dice011;
    public AudioClip dice012;
    public AudioClip dice013;
    public AudioClip dice014;

    public AudioClip point1;
    public AudioClip point2;
    public AudioClip point3;
    public AudioClip point4;
    public AudioClip point5;
    public AudioClip point6;


    #endregion

    #region Private Variables
    private float xInput;
    private float yInput;
    private bool isRotating;

    private Vector3 target;
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

        if (Input.GetKeyDown(KeyCode.Space) && !isRotating)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.AddForce(0f, moveSpeed, 0f);
        }
    }

    // Move the player
    private void Move()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            rb.AddForce(new Vector3(xInput, 0f, yInput) * moveSpeed);
        }
    }

    private void OnCollisionEnter()
    {
        int num = Random.Range(1, 15);
        if (num == 1)
        {
            GetComponent<AudioSource>().clip = dice001;
        }
        else if (num == 2)
        {
            GetComponent<AudioSource>().clip = dice002;
        }
        else if (num == 3)
        {
            GetComponent<AudioSource>().clip = dice003;
        }
        else if (num == 4)
        {
            GetComponent<AudioSource>().clip = dice004;
        }
        else if (num == 5)
        {
            GetComponent<AudioSource>().clip = dice005;
        }
        else if (num == 6)
        {
            GetComponent<AudioSource>().clip = dice006;
        }
        else if (num == 7)
        {
            GetComponent<AudioSource>().clip = dice007;
        }
        else if (num == 8)
        {
            GetComponent<AudioSource>().clip = dice008;
        }
        else if (num == 9)
        {
            GetComponent<AudioSource>().clip = dice009;
        }
        else if (num == 10)
        {
            GetComponent<AudioSource>().clip = dice010;
        }
        else if (num == 11)
        {
            GetComponent<AudioSource>().clip = dice011;
        }
        else if (num == 12)
        {
            GetComponent<AudioSource>().clip = dice012;
        }
        else if (num == 13)
        {
            GetComponent<AudioSource>().clip = dice013;
        }
        else if (num == 14)
        {
            GetComponent<AudioSource>().clip = dice014;
        }

        GetComponent<AudioSource>().Play();
    }
    #endregion
}