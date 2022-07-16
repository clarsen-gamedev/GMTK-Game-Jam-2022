using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DieSideColliderCheck : MonoBehaviour
{
    #region Public Variables
    public int sideFace;    // The side of the die this script is attached to
    public Text textUI;     // Reference to the debug text on the UI
    #endregion

    #region Private Variables
    #endregion

    #region Functions
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")    // If the collider is part of the ground layer...
        {
            if (sideFace == 1)
            {
                textUI.text = "Face Up: 6";
            }

            else if (sideFace == 2)
            {
                textUI.text = "Face Up: 5";
            }

            else if (sideFace == 3)
            {
                textUI.text = "Face Up: 4";
            }

            else if (sideFace == 4)
            {
                textUI.text = "Face Up: 3";
            }

            else if (sideFace == 5)
            {
                textUI.text = "Face Up: 2";
            }

            else if (sideFace == 6)
            {
                textUI.text = "Face Up: 1";
            }
        }

        else
        {
            textUI.text = "Face Up: ?";
        }
    }
    #endregion
}