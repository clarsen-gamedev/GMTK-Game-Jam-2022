using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenManager : MonoBehaviour
{
    #region Public Variables
    [Header("UI Screens")]
    [SerializeField] GameObject titleUI;
    [SerializeField] GameObject instructionsUI;
    [SerializeField] GameObject creditsUI;

    [HideInInspector] public enum UIScreens { TITLE, INSTRUCTIONS, CREDITS, NONE };
    [HideInInspector] public UIScreens currentScreen;
    #endregion

    #region Functions
    // Start is called before the first frame update
    void Start()
    {
        UISwitch(UIScreens.TITLE);  // Start on the title screen
    }
    #endregion

    // Play Game Button
    public void PlayGame()
    {
        UISwitch(UIScreens.INSTRUCTIONS);   // Switch to the instructions screen
    }

    // Credits Button
    public void Credits()
    {
        UISwitch(UIScreens.CREDITS);    // Switch to the credits screen
    }

    // Menu Button
    public void Menu()
    {
        UISwitch(UIScreens.TITLE);  // Switch to the title screen
    }

    // Quit Button
    public void QuitGame()
    {
        Application.Quit(); // Quit the game
    }

    // Function for enabling and disabling specific UI screens
    public void UISwitch(UIScreens screen)
    {
        if (screen == UIScreens.TITLE)
        {
            titleUI.SetActive(true);
            instructionsUI.SetActive(false);
            creditsUI.SetActive(false);

            currentScreen = UIScreens.TITLE;
        }

        else if (screen == UIScreens.INSTRUCTIONS)
        {
            titleUI.SetActive(false);
            instructionsUI.SetActive(true);
            creditsUI.SetActive(false);

            currentScreen = UIScreens.TITLE;
        }

        else if (screen == UIScreens.CREDITS)
        {
            titleUI.SetActive(false);
            instructionsUI.SetActive(false);
            creditsUI.SetActive(true);

            currentScreen = UIScreens.CREDITS;
        }
    }
}