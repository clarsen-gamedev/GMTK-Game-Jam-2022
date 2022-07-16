// Name: Hazard.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Script which controls the behaviour of the hazard hitboxes in the level

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    #region Private Variables
    private GameManager gameManager;    // Reference to the GameManager script
    #endregion

    #region Functions
    // Start is called before the first frame update
    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
    }

    // Check to see if the player has entered the hazard hitbox
    private void OnTriggerEnter(Collider collision)
    {
        // If the player has collided with the hazard
        if (collision.tag == "Player")
        {
            Time.timeScale = 0f;    // Pause time
            gameManager.GameOver(); // End the current run
        }
    }
    #endregion
}