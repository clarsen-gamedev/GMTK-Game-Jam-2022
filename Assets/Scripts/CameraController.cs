// Name: CameraController.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Have the attached camera follow a declared target

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Public Variables
    public GameObject target;               // Reference to what the camera will follow
    public float xOffset, yOffset, zOffset; // Controls the offset of the camera as it follows the target
    #endregion

    #region Functions
    // Update is called once per frame
    void Update()
    {
        transform.position = target.transform.position + new Vector3(xOffset, yOffset, zOffset);
        transform.LookAt(target.transform.position);
    }
    #endregion
}