// Name: CameraFollow.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Script used for moving the camera alongside the player

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    #region Public Variables
    public Transform player;            // Reference to the position of the player
    public Vector3 offset;              // Offset to apply to the canera's position
    public float smoothSpeed = 0.125f;  // Speed that the camera follows at
    #endregion

    #region Functions
    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y, 0) + offset; // Move the camera towards the player
    }
    #endregion
}