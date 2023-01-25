using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private PlayType _type;
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void Play() //Play the game adn go to the first track
    {
        _type._playtype = "singleplayer";
        SceneManager.LoadScene("Nederland Singplayer");
    }

    public void Splitscreen()
    {
        _type._playtype = "multiplayer";
        SceneManager.LoadScene("Nederland Splitscreen Multiplayer");
    }
}
