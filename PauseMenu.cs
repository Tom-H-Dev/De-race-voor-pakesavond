using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    private bool _isMenuActive = false;

    void Update()
    {
        //The pause menu when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape) && !_isMenuActive)
        {
            //Tiem scale will be set to 0
            Time.timeScale = 0;
            //The pause menu item will be set to active
            _pauseMenu.SetActive(true);
            //The bool that game is paused
            _isMenuActive = true;
        }
    }

    public void BackToMenu() //sends you back to the main menu
    {
        Time.timeScale = 1;
        GameManager.instance._gameIsActive = false;
        SceneManager.LoadScene("Main Menu");
    }

    public void ResumeGame() //When the game is chosen to resume the game will be set to an active time sclae and the menu will be deactivated
    {
        Time.timeScale = 1;
        _pauseMenu.SetActive(false);
        _isMenuActive = false;
    }

    public void ExitGame()
    {
#if UNITY_EDITOR //If the game is open in the unity editor
        UnityEditor.EditorApplication.ExitPlaymode();
#else //If the game is open in build
        Application.Quit();
#endif
    }
}
