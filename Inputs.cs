using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputs", menuName = "Player Inputs", order = 0)]
public class Inputs : ScriptableObject
{
    public string _forwardMovementLeft = "w";
    public string _backwardsMovementLeft = "s";
    public string _rightMovementLeft = "d";
    public string _leftMovementLeft = "a";

    public string _forwardMovementRight = "i";
    public string _backwardsMovementRight = "k";
    public string _rightMovementRight = "l";
    public string _leftMovementRight = "j";
}
