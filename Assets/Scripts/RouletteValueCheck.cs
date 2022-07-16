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

    #region Functions
    private void Awake()
    {
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
                gateAnimator.SetTrigger("GateOpen");
                gameObject.SetActive(false);
            }
        }
    }
    #endregion
}