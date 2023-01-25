using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool _canDrive;
    public bool _gameIsActive = false;
    public bool _canTime = false;
    public List<float> _lapTimesRight;
    public List<float> _lapTimesLeft;

    public PlayType _playType;
    public bool _spain = false;

    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        
    }
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetFloat(string l_keyName, float l_value)
    {
        PlayerPrefs.SetFloat(l_keyName, l_value);
    }

    public float GetFloat(string l_keyName)
    {
        return PlayerPrefs.GetFloat(l_keyName);
    }
}
