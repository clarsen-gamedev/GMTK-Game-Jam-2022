// Title: GameManager.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Contains functions which control various aspects of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public and Serialized Variables
    [Header("Gameplay Objects")]
    public GameObject player;     // Reference to the player object in the scene
    public Vector3 startPosition; // Starting position for the player on game start

    [Header("Roulette Colliders")]
    public GameObject[] outerRingColliders;
    public GameObject[] middleRingColliders;
    //public GameObject[] innerRingColliders;

    [Header("Animations")]
    public Animator gateAnimatorOuter;
    public Animator gateAnimatorInner;

    [Header("UI Elements")]
    [SerializeField] GameObject gameplayUI; // UI screen for gameplay
    [SerializeField] GameObject pauseUI;    // UI screen for pause screen
    [SerializeField] GameObject gameOverUI; // UI screen for game over screen
    [SerializeField] Text gameOverText;     // Text for the game over screen

    [Header("Controls")]
    [SerializeField] KeyCode pauseButton = KeyCode.Escape;  // Reference to the key responsible for pausing the game

    // Hidden from Inspector
    [HideInInspector] public enum UIScreens { GAME, PAUSE, GAMEOVER, MAX };
    #endregion

    #region Private Variables
    private bool isPaused = false;
    private int cycleCount = 0;
    #endregion

    #region Functions
    // Awake is called on the first possible frame
    private void Awake()
    {
        ResetGame();
    }

    // Update is called once per frame
    private void Update()
    {
        // Pause the game
        if (Input.GetKeyDown(pauseButton))
        {
            if (isPaused)   // If game is paused...
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }

        // Game Over Screen Message
        if (player.GetComponent<CheckDieValue>().currentSide == 1)
        {
            gameOverText.text = "Oof";
        }

        else if (player.GetComponent<CheckDieValue>().currentSide == 2)
        {
            gameOverText.text = "Big oof";
        }

        else if (player.GetComponent<CheckDieValue>().currentSide == 3)
        {
            gameOverText.text = "Git gud scrub";
        }

        else if (player.GetComponent<CheckDieValue>().currentSide == 4)
        {
            gameOverText.text = "Could have been better";
        }

        else if (player.GetComponent<CheckDieValue>().currentSide == 5)
        {
            gameOverText.text = "You call that a roll?";
        }

        else
        {
            gameOverText.text = "By all accounts, that was bad";
        }
    }

    // Once a value check has been acomplished, randomly choose a new spot, and also add hazards if far enough in the game
    public void NextGoal()
    {
        // Check which turn it is in the current game
        if (cycleCount == 0)    // If 1st turn, spawn in outer ring
        {
            outerRingColliders[Random.Range(0, outerRingColliders.Length)].gameObject.SetActive(true);  // Activate a random collider in the outer ring
        }

        else if (cycleCount == 1)   // If 2nd turn, spawn in middle ring
        {
            middleRingColliders[Random.Range(0, middleRingColliders.Length)].gameObject.SetActive(true);    // Activate a random collider in the middle ring
        }
        
        else if (cycleCount == 2)   // If 3rd turn, spawn in inner ring
        {
            //innerRingColliders[Random.Range(0, innerRingColliders.Length)].gameObject.SetActive(true);  // Activate a random collider in the outer ring
        }

        else    // If 4th or higher, randomly select anywhere on the wheel and start activating a random number of hazard squares
        {

        }

        cycleCount++;   // Increase the cycle count by 1
    }

    // Call this function to unpause the game
    public void ResumeGame()
    {
        UISwitch(UIScreens.GAME);   // Switch to game screen
        Time.timeScale = 1f;        // Resume time
        isPaused = false;           // Resume game
        Debug.Log("GAME RESUMED");
    }

    // Call this function to pause the game
    public void PauseGame()
    {
        UISwitch(UIScreens.PAUSE);  // Switch to pause screen
        Time.timeScale = 0f;        // Pause time
        isPaused = true;            // Pause game
        Debug.Log("GAME PAUSED");
    }

    // Call this function to reinitialize the game
    public void ResetGame()
    {
        // Reset the UI
        UISwitch(UIScreens.GAME);   // Switch screen
        Time.timeScale = 1f;        // Resume time
        isPaused = false;           // Resume game

        // Reset Variables
        cycleCount = 0; // Reset the cycle counter

        // Reset the player
        player.transform.position = startPosition;                  // Reset player position
        player.transform.rotation = Quaternion.identity;            // Reset player rotation
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;   // Reset player movement
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        // Reset Roulette Colliders
        foreach (GameObject collider in outerRingColliders) // Outer Ring
        {
            collider.gameObject.SetActive(false);
        }
        foreach (GameObject collider in middleRingColliders)    // Middle Ring
        {
            collider.gameObject.SetActive(false);
        }
        //foreach (GameObject collider in innerRingColliders) // Inner Ring
        //{
        //    collider.gameObject.SetActive(false);
        //}
        NextGoal(); // Set the next goal

        // Reset Animations
        gateAnimatorOuter.Play("GateClosed");
        gateAnimatorOuter.SetBool("GateOpen", false);

        //gateAnimatorInner.Play("GateClosed");
    }

    // Call this function when the player dies
    public void GameOver()
    {
        Time.timeScale = 0f;            // Pause time
        UISwitch(UIScreens.GAMEOVER);   // Switch to the Game Over Screen
    }

    // Call this function to quit the game
    public void QuitGame()
    {
        Application.Quit(); // Exit the game
    }

    // Function for enabling and disabling specific UI screens
    public void UISwitch(UIScreens screen)
    {
        if (screen == UIScreens.GAME)
        {
            gameplayUI.SetActive(true);
            pauseUI.SetActive(false);
            gameOverUI.SetActive(false);
        }

        else if (screen == UIScreens.PAUSE)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(true);
            gameOverUI.SetActive(false);
        }

        else if (screen == UIScreens.GAMEOVER)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(false);
            gameOverUI.SetActive(true);
        }
    }
    #endregion
}