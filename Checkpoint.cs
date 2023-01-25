using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //If the GameObject that went trough the trigger collider 
        if (other.gameObject.tag == "Player" && other.gameObject.GetComponent<Car>()._isLeftCar)
        {
            //Adds the currenent checkpoint to the List of checkpoints
            CheckpointManager.instance._checkpointsLeft.Add(gameObject);
        }
        else if (other.gameObject.tag == "Player" && !other.gameObject.GetComponent<Car>()._isLeftCar)
        {
            //Adds the currenent checkpoint to the List of checkpoints
            CheckpointManager.instance._checkpointsRight.Add(gameObject);
        }
    }
}
