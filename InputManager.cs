using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _InputForwardMovement, _InputLeftMovement, _InputRightMovement, _InputBackwardsMovement;
    [SerializeField] private TMP_InputField _InputForwardMovementRight, _InputLeftMovementRight, _InputRightMovementRight, _InputBackwardsMovementRight;
    [SerializeField] private Inputs _playerInputs;

    private string _keyInputForward = "forward", _keyInputBackwards = "backward", _keyInputRight = "right", _keyInputLeft = "left";
    private string _keyInputForwardRight = "forwardright", _keyInputBackwardsRight = "backwardright", _keyInputRightRight = "rightright", _keyInputLeftRight = "leftright";

    private void Start()
    {
        RecoverControls();
        SetInputText();
    }

    private void SetInputText()
    {
        //sets the initial text of the saved inputs
        _InputForwardMovement.text = _playerInputs._forwardMovementLeft;
        _InputBackwardsMovement.text = _playerInputs._backwardsMovementLeft;
        _InputRightMovement.text = _playerInputs._rightMovementLeft;
        _InputLeftMovement.text = _playerInputs._leftMovementLeft;

        _InputForwardMovementRight.text = _playerInputs._forwardMovementRight;
        _InputBackwardsMovementRight.text = _playerInputs._backwardsMovementRight;
        _InputRightMovementRight.text = _playerInputs._rightMovementRight;
        _InputLeftMovementRight.text = _playerInputs._leftMovementRight;
    }

    /// <summary>
    /// A simple system for changing the controls that are located in a scriptable object
    /// </summary>
    /// <param name="key"></The key for the playerprefs file where the movement input is saved for the next time>
    /// <param name="field"></The string that is in the scriptable object where all the movement keys are stored>
    /// <param name="text"></The text component from the input field>
    public void OnInputValueChange(string key, ref string field, string text)
    {
        field = text;
        SetString(key, field);
    }

    private void RecoverControls()
    {
        if (GetString(_keyInputForward) == "" || GetString(_keyInputForwardRight) == "")
        {
            print("first controlls");

            _playerInputs._forwardMovementLeft = "w";
            _playerInputs._backwardsMovementLeft = "s";
            _playerInputs._rightMovementLeft = "d";
            _playerInputs._leftMovementLeft = "a";

            _playerInputs._forwardMovementRight = "i";
            _playerInputs._backwardsMovementRight = "k";
            _playerInputs._rightMovementRight = "l";
            _playerInputs._leftMovementRight = "j";
        }
        else
        {
            //gets all the movment keys saved in the playerprefs file to the corresponding inputs
            _playerInputs._forwardMovementLeft = GetString(_keyInputForward);
            _playerInputs._backwardsMovementLeft = GetString(_keyInputBackwards);
            _playerInputs._rightMovementLeft = GetString(_keyInputRight);
            _playerInputs._leftMovementLeft = GetString(_keyInputLeft);

            _playerInputs._forwardMovementRight = GetString(_keyInputForwardRight);
            _playerInputs._backwardsMovementRight = GetString(_keyInputBackwardsRight);
            _playerInputs._rightMovementRight = GetString(_keyInputRightRight);
            _playerInputs._leftMovementRight = GetString(_keyInputLeftRight);
        }
    }

    //the player prefs set and get for the input values
    public void SetString(string KeyName, string Value)
    {
        PlayerPrefs.SetString(KeyName, Value);
    }

    public string GetString(string KeyName)
    {
        return PlayerPrefs.GetString(KeyName);
    }

    //the listeners for the inputfield when they get changed
    private void OnEnable()
    {
        _InputForwardMovement.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputForward, ref _playerInputs._forwardMovementLeft, _InputForwardMovement.text); });
        _InputBackwardsMovement.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputBackwards, ref _playerInputs._backwardsMovementLeft, _InputBackwardsMovement.text); });
        _InputRightMovement.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputRight, ref _playerInputs._rightMovementLeft, _InputRightMovement.text); });
        _InputLeftMovement.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputLeft, ref _playerInputs._leftMovementLeft, _InputLeftMovement.text); });

        _InputForwardMovementRight.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputForwardRight, ref _playerInputs._forwardMovementRight, _InputForwardMovementRight.text); });
        _InputBackwardsMovementRight.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputBackwardsRight, ref _playerInputs._backwardsMovementRight, _InputBackwardsMovementRight.text); });
        _InputRightMovementRight.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputRightRight, ref _playerInputs._rightMovementRight, _InputRightMovementRight.text); });
        _InputLeftMovementRight.onValueChanged.AddListener(delegate { OnInputValueChange(_keyInputLeftRight, ref _playerInputs._leftMovementRight, _InputLeftMovementRight.text); });

    }

    private void OnDisable()
    {
        _InputForwardMovement.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputForward, ref _playerInputs._forwardMovementRight, _InputForwardMovement.text); });
        _InputBackwardsMovement.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputBackwards, ref _playerInputs._backwardsMovementRight, _InputBackwardsMovement.text); });
        _InputRightMovement.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputRight, ref _playerInputs._rightMovementRight, _InputRightMovement.text); });
        _InputLeftMovement.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputLeft, ref _playerInputs._leftMovementRight, _InputLeftMovement.text); });

        _InputForwardMovementRight.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputForwardRight, ref _playerInputs._forwardMovementRight, _InputForwardMovementRight.text); });
        _InputBackwardsMovementRight.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputBackwardsRight, ref _playerInputs._backwardsMovementRight, _InputBackwardsMovementRight.text); });
        _InputRightMovementRight.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputRightRight, ref _playerInputs._rightMovementRight, _InputRightMovementRight.text); });
        _InputLeftMovementRight.onValueChanged.RemoveListener(delegate { OnInputValueChange(_keyInputLeftRight, ref _playerInputs._leftMovementRight, _InputLeftMovementRight.text); });
    }
}
