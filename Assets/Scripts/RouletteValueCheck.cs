using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteValueCheck : MonoBehaviour
{
    #region Public Variables
    public int requiredFace;    // Which face of the die is needed to pass the check
    #endregion

    #region Private Variables
    #endregion

    #region Functions
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")   // If the player enters the hitbox...
        {
            // If the face matches the required value...
            if (collider.gameObject.GetComponent<CheckDieValue>().currentSide == requiredFace)
            {
                Destroy(gameObject);    // Destroy this collider
            }
        }
    }
    #endregion
}