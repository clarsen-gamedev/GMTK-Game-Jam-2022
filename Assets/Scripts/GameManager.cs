// Title: GameManager.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Contains functions which control various aspects of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Public and Serialized Variables
    [Header("Gameplay Objects")]
    public GameObject player;                       // Reference to the player object in the scene
    public Vector3 startPosition;                   // Starting position for the player on game start
    public int finalScore;                          // The score the player must achieve to win the game
    [HideInInspector] public int playerScore;       // The player's score
    [HideInInspector] public int colliderCount = 0; // Keeps track of how many colliders the player has hit during their session

    [Header("Roulette Colliders")]
    public GameObject[] outerRingColliders;
    public GameObject[] middleRingColliders;
    public GameObject[] innerRingColliders;

    [Header("Animations")]
    public Animator gateAnimatorOuter;
    public Animator gateAnimatorInner;

    [Header("Materials")]
    public Material goal;
    public Material hazard;

    [Header("UI Elements")]
    [SerializeField] GameObject gameplayUI;         // UI screen for gameplay
    [SerializeField] GameObject pauseUI;            // UI screen for pause screen
    [SerializeField] GameObject gameOverUI;         // UI screen for game over screen
    [SerializeField] GameObject victoryUI;          // UI screen for the victory screen
    [SerializeField] Text gameOverText;             // Text for the game over screen
    [SerializeField] Text scoreText;                // Text for the player score
    [SerializeField] Text colliderCountText;        // Text for the collider count

    [Header("Controls")]
    [SerializeField] KeyCode pauseButton = KeyCode.Escape;  // Reference to the key responsible for pausing the game

    // Hidden from Inspector
    [HideInInspector] public enum UIScreens { GAME, PAUSE, GAMEOVER, VICTORY, NONE };
    public UIScreens currentScreen;
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
        // Display the score
        scoreText.text = "Score: " + playerScore;

        // Display the number of spaces hit
        colliderCountText.text = "Spaces Hit: " + colliderCount;

        // Pause the game
        if (Input.GetKeyDown(pauseButton))
        {
            // Only allow pausing if in game or paused
            if (currentScreen == UIScreens.GAME || currentScreen == UIScreens.PAUSE)
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
            cycleCount++;   // Increase the cycle count by 1
        }

        else if (cycleCount == 1)   // If 2nd turn, spawn in middle ring
        {
            middleRingColliders[Random.Range(0, middleRingColliders.Length)].gameObject.SetActive(true);    // Activate a random collider in the middle ring
            cycleCount++;   // Increase the cycle count by 1
        }
        
        else if (cycleCount == 2)   // If 3rd turn, spawn in inner ring
        {
            innerRingColliders[Random.Range(0, innerRingColliders.Length)].gameObject.SetActive(true);  // Activate a random collider in the outer ring
            cycleCount++;   // Increase the cycle count by 1
        }

        else    // Soft reset the game
        {
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
            foreach (GameObject collider in innerRingColliders) // Inner Ring
            {
                collider.gameObject.SetActive(false);
            }

            // Reset Animations
            gateAnimatorOuter.Play("GateClosed");
            gateAnimatorOuter.SetBool("GateOpen", false);

            gateAnimatorInner.Play("GateClosed");
            gateAnimatorInner.SetBool("GateOpen", false);

            // Reset cycle
            cycleCount = 0;

            NextGoal();
        }
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
        cycleCount = 0;     // Reset the cycle counter
        playerScore = 0;    // Reset player score
        colliderCount = 0;  // Reset the collider counter

        // Reset the player
        player.transform.position = startPosition;                  // Reset player position
        player.transform.rotation = Quaternion.identity;            // Reset player rotation
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;   // Reset player movement
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        player.GetComponent<PlayerController>().enabled = true;     // Re enable player controls

        // Reset Roulette Colliders
        foreach (GameObject collider in outerRingColliders) // Outer Ring
        {
            collider.gameObject.SetActive(false);
        }
        foreach (GameObject collider in middleRingColliders)    // Middle Ring
        {
            collider.gameObject.SetActive(false);
        }
        foreach (GameObject collider in innerRingColliders) // Inner Ring
        {
            collider.gameObject.SetActive(false);
        }

        NextGoal(); // Set the next goal

        // Reset Animations
        gateAnimatorOuter.Play("GateClosed");
        gateAnimatorOuter.SetBool("GateOpen", false);

        gateAnimatorInner.Play("GateClosed");
        gateAnimatorInner.SetBool("GateOpen", false);
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
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameTitle");    // Load the title scene
    }

    // Function for enabling and disabling specific UI screens
    public void UISwitch(UIScreens screen)
    {
        if (screen == UIScreens.GAME)
        {
            gameplayUI.SetActive(true);
            pauseUI.SetActive(false);
            gameOverUI.SetActive(false);
            victoryUI.SetActive(false);

            currentScreen = UIScreens.GAME;
        }

        else if (screen == UIScreens.PAUSE)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(true);
            gameOverUI.SetActive(false);
            victoryUI.SetActive(false);

            currentScreen = UIScreens.PAUSE;
        }

        else if (screen == UIScreens.GAMEOVER)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(false);
            gameOverUI.SetActive(true);
            victoryUI.SetActive(false);

            currentScreen = UIScreens.GAMEOVER;
        }

        else if (screen == UIScreens.VICTORY)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(false);
            gameOverUI.SetActive(false);
            victoryUI.SetActive(true);

            currentScreen = UIScreens.VICTORY;
        }
    }
    #endregion
}