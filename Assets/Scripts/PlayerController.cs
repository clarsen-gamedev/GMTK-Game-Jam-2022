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