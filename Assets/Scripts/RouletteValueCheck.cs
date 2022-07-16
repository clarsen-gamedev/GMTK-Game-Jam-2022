// Name: RouletteValueCheck.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Control the colliders on the roulette wheel

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteValueCheck : MonoBehaviour
{
    #region Public Variables
    [Header("Variables")]
    public int requiredFace;        // Which face of the die is needed to pass the check
    public bool isActive;           // If the collider is active or not
    public Animator gateAnimator;   // Animator attached to the gate which gets lowered

    [Header("Materials")]
    [SerializeField] Material inactive;
    [SerializeField] Material active;
    #endregion

    #region Private Variables
    private GameManager gameManager;    // Reference to the game manager in the scene
    #endregion

    #region Functions
    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<GameManager>();
        isActive = false;
    }

    private void Update()
    {
        if (isActive)
        {
            gameObject.GetComponent<MeshRenderer>().material = active;
        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material = inactive;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player" && isActive)   // If the player enters the hitbox of an active collider...
        {
            // If the face matches the required value...
            if (collider.gameObject.GetComponent<CheckDieValue>().currentSide == requiredFace)
            {
                if (gateAnimator.GetCurrentAnimatorStateInfo(0).IsName("GateClose"))    // If the gate is still closed...
                {
                    gateAnimator.SetTrigger("GateOpen");    // Open the gate
                }
                gameObject.SetActive(false);    // Disable the current collider

                //gameManager.NextGoal(); // Select the next collider to activate
            }
        }
    }
    #endregion
}