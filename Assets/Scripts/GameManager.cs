// Title: GameManager.cs
// Author: Connor Larsen
// Date: 07/15/2022
// Description: Contains functions which control various aspects of the game

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public and Serialized Variables
    [Header("Gameplay Objects")]
    [SerializeField] GameObject player;     // Reference to the player object in the scene
    [SerializeField] Vector3 startPosition; // Starting position for the player on game start

    [Header("UI Elements")]
    [SerializeField] GameObject gameplayUI; // UI screen for gameplay
    [SerializeField] GameObject pauseUI;    // UI screen for pause screen

    [Header("Controls")]
    [SerializeField] KeyCode pauseButton = KeyCode.Escape;  // Reference to the key responsible for pausing the game

    // Hidden from Inspector
    [HideInInspector] public enum UIScreens { GAME, PAUSE, GAMEOVER, MAX };
    #endregion

    #region Private Variables
    private bool isPaused = false;
    #endregion

    #region Functions
    // Awake is called on the first possible frame
    private void Awake()
    {
        UISwitch(UIScreens.GAME);   // Start on the gameplay screen
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

        // Reset the player
        player.transform.position = startPosition;                  // Reset player position
        player.transform.rotation = Quaternion.identity;            // Reset player rotation
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;   // Reset player movement
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
        }

        else if (screen == UIScreens.PAUSE)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(true);
        }

        else if (screen == UIScreens.GAMEOVER)
        {
            gameplayUI.SetActive(false);
            pauseUI.SetActive(false);
        }
    }
    #endregion
}