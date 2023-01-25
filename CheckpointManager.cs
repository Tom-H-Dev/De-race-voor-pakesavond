using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager instance;

    //The List of checkpoints
    public List<GameObject> _checkpointsRight;
    public List<GameObject> _checkpointsLeft;

    private void Awake()
    {
            instance = this;
    }

}
