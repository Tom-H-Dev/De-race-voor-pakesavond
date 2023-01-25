using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartFinish : MonoBehaviour
{
    public static StartFinish instance;

    public float _localLapTime;
    private float allTimesCombinedLeft;

    public string lap1right = "lap1right", lap2right = "lap2right", lap3right = "lap3right";
    public string lap1left = "lap1left", lap2left = "lap2left", lap3left = "lap3left";

    public string lap1rightspain = "lap1rightspain", lap2rightspain = "lap2rightspain", lap3rightspain = "lap3rightspain";
    public string lap1leftspain = "lap1leftspain", lap2leftspain = "lap2leftspain", lap3leftspain = "lap3leftspain";

    [SerializeField] private GameObject _endScene;



    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (GameManager.instance._playType._playtype == "multiplayer")
        {
            UIManager.instance.UpdateLapUIRight(1);
            UIManager.instance.UpdateLapUILeft(1);
            StartCoroutine(StartTheGame());
        }
        else
        {
            UIManager.instance.UpdateLapUILeft(1);
            StartCoroutine(StartTheGame());
        }


    }
    private int _finishedplayers;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Car>(out Car carComponent))
        {
            //If you are not on the final lap
            if (carComponent._lap != 3)
            {
                if (carComponent._isLeftCar)
                {
                    //Check if all the checkpoints have been touched
                    if (CheckpointManager.instance._checkpointsLeft.Count >= 4)
                    {
                        //adds a lap if all the checkpoints have been touched
                        carComponent._lap += 1;
                        //Updates the UI for the lapcounter
                        UIManager.instance.UpdateLapUILeft(carComponent._lap);
                        //Tells the UIManager that all the checkpoints have been touched
                        UIManager.instance.MissedCheckpoint(false);
                        //cleares the list of all touched checkpoints
                        CheckpointManager.instance._checkpointsLeft.Clear();

                        //Add current laptime
                        GameManager.instance._lapTimesLeft.Add(carComponent._lapTime);
                        //resets the laptime
                        carComponent._lapTime = 0f;
                    }
                    else //If 1 or more of the checkpoints have not been touched it will show a message on screen for 4 seconds
                    {
                        StartCoroutine(MissedLapTextTime());
                        CheckpointManager.instance._checkpointsLeft.Clear();
                    }
                }
                else if (!carComponent._isLeftCar)
                {
                    //Check if all the checkpoints have been touched
                    if (CheckpointManager.instance._checkpointsRight.Count >= 4)
                    {
                        //adds a lap if all the checkpoints have been touched
                        carComponent._lap += 1;
                        //Updates the UI for the lapcounter
                        UIManager.instance.UpdateLapUIRight(carComponent._lap);
                        //Tells the UIManager that all the checkpoints have been touched
                        UIManager.instance.MissedCheckpoint(false);
                        //cleares the list of all touched checkpoints
                        CheckpointManager.instance._checkpointsRight.Clear();

                        //Add current laptime
                        GameManager.instance._lapTimesRight.Add(carComponent._lapTime);
                        //resets the laptime
                        carComponent._lapTime = 0f;
                    }
                    else //If 1 or more of the checkpoints have not been touched it will show a message on screen for 4 seconds
                    {
                        StartCoroutine(MissedLapTextTime());
                        CheckpointManager.instance._checkpointsRight.Clear();
                    }
                }
            }
            else //If it is the final lap
            {
                _finishedplayers++;


                if (carComponent._isLeftCar)
                {
                    //Check is all checkpoints have been reached
                    if (CheckpointManager.instance._checkpointsLeft.Count >= 4)
                    {

                        //Adds the last laptime to the list
                        GameManager.instance._lapTimesLeft.Add(carComponent._lapTime);
                        //resets the laptime
                        carComponent._lapTime = 0f;

                        //Checks if there is a new highscore in time
                        CheckIfLApsIsNewHighscore(other.gameObject);
                        //Sets the current lap times on the UI
                        UIManager.instance.SetRaceLapTimesLeft();
                    }
                    else //If 1 or more of the checkpoints have not been touched it will show a message on screen for 4 seconds
                    {
                        StartCoroutine(MissedLapTextTime());
                        CheckpointManager.instance._checkpointsLeft.Clear();
                    }
                }
                else if (!carComponent._isLeftCar)
                {
                    //Check is all checkpoints have been reached
                    if (CheckpointManager.instance._checkpointsRight.Count >= 4)
                    {

                        //Adds the last laptime to the list
                        GameManager.instance._lapTimesRight.Add(carComponent._lapTime);
                        //resets the laptime
                        carComponent._lapTime = 0f;

                        //Sets the current lap times on the UI
                        UIManager.instance.SetRaceLapTimesRight();
                        //Checks if there is a new highscore in time
                        CheckIfLApsIsNewHighscore(other.gameObject);
                    }
                    else //If 1 or more of the checkpoints have not been touched it will show a message on screen for 4 seconds
                    {
                        StartCoroutine(MissedLapTextTime());
                        CheckpointManager.instance._checkpointsLeft.Clear();
                    }
                }

                if (_finishedplayers == 1 && GameManager.instance._playType._playtype == "multiplayer")
                {
                    StartCoroutine(DNF());
                }
                else if (_finishedplayers >= 2 && GameManager.instance._playType._playtype == "multiplayer")
                {
                    Debug.Log("All players reached finish");
                    //The end screen for the race will pop up
                    _endScene.SetActive(true);

                    //sets the timescale t 0
                    Time.timeScale = 0;
                }
                else
                {
                    //The end screen for the race will pop up
                    _endScene.SetActive(true);

                    //sets the timescale t 0
                    Time.timeScale = 0;
                }
            }
        }
    }

    private void CheckIfLApsIsNewHighscore(GameObject player)
    {
        if (GameManager.instance._spain)
        {
            if (player.GetComponent<Car>()._isLeftCar)
            {
                Debug.Log("New highscore for left car!");
                //Gets all the saved laptimes
                float lap1time = GameManager.instance.GetFloat(lap1leftspain);
                float lap2time = GameManager.instance.GetFloat(lap2leftspain);
                float lap3time = GameManager.instance.GetFloat(lap3leftspain);

                //Combines both all the current laptimes and the saved laptimes
                allTimesCombinedLeft = GameManager.instance._lapTimesLeft[0] + GameManager.instance._lapTimesLeft[1] + GameManager.instance._lapTimesLeft[2];
                float currentSavedTimes = lap1time + lap2time + lap3time;

                //Checks wich laptimes combination is smaller and check if the saved times are not 0
                if (allTimesCombinedLeft < currentSavedTimes || currentSavedTimes == 0)
                {
                    //If a new best time is reached the laptimes will be saved to the PlayerPrefs
                    GameManager.instance.SetFloat(lap1leftspain, GameManager.instance._lapTimesLeft[0]);
                    GameManager.instance.SetFloat(lap2leftspain, GameManager.instance._lapTimesLeft[1]);
                    GameManager.instance.SetFloat(lap3leftspain, GameManager.instance._lapTimesLeft[2]);

                    lap1time = GameManager.instance.GetFloat(lap1leftspain);
                    lap2time = GameManager.instance.GetFloat(lap2leftspain);
                    lap3time = GameManager.instance.GetFloat(lap3leftspain);

                    //The UI will be updated for the laptimes wich are best
                    UIManager.instance.UpdateBestLapTimesLeft(lap1time, lap2time, lap3time);
                    allTimesCombinedLeft = 0;
                }
                else
                {
                    //if there is no new best reached time the ui will be updated with the old saved times
                    UIManager.instance.UpdateBestLapTimesLeft(lap1time, lap2time, lap3time);
                }
            }
            else if (!player.GetComponent<Car>()._isLeftCar)
            {
                Debug.Log("New highscore for right car!");
                //Gets all the saved laptimes
                float lap1time = GameManager.instance.GetFloat(lap1rightspain);
                float lap2time = GameManager.instance.GetFloat(lap2rightspain);
                float lap3time = GameManager.instance.GetFloat(lap3rightspain);

                //Combines both all the current laptimes and the saved laptimes
                allTimesCombinedLeft = GameManager.instance._lapTimesRight[0] + GameManager.instance._lapTimesRight[1] + GameManager.instance._lapTimesRight[2];
                float currentSavedTimes = lap1time + lap2time + lap3time;

                //Checks wich laptimes combination is smaller and check if the saved times are not 0
                if (allTimesCombinedLeft < currentSavedTimes || currentSavedTimes == 0)
                {
                    //If a new best time is reached the laptimes will be saved to the PlayerPrefs
                    GameManager.instance.SetFloat(lap1rightspain, GameManager.instance._lapTimesRight[0]);
                    GameManager.instance.SetFloat(lap2rightspain, GameManager.instance._lapTimesRight[1]);
                    GameManager.instance.SetFloat(lap3rightspain, GameManager.instance._lapTimesRight[2]);

                    lap1time = GameManager.instance.GetFloat(lap1rightspain);
                    lap2time = GameManager.instance.GetFloat(lap2rightspain);
                    lap3time = GameManager.instance.GetFloat(lap3rightspain);

                    //The UI will be updated for the laptimes wich are best
                    UIManager.instance.UpdateBestLapTimesRight(lap1time, lap2time, lap3time);
                    allTimesCombinedLeft = 0;
                }
                else
                {
                    //if there is no new best reached time the ui will be updated with the old saved times
                    UIManager.instance.UpdateBestLapTimesRight(lap1time, lap2time, lap3time);
                }
            }
        }
        else
        {
            if (player.GetComponent<Car>()._isLeftCar)
            {
                Debug.Log("New highscore for left car!");
                //Gets all the saved laptimes
                float lap1time = GameManager.instance.GetFloat(lap1left);
                float lap2time = GameManager.instance.GetFloat(lap2left);
                float lap3time = GameManager.instance.GetFloat(lap3left);

                //Combines both all the current laptimes and the saved laptimes
                allTimesCombinedLeft = GameManager.instance._lapTimesLeft[0] + GameManager.instance._lapTimesLeft[1] + GameManager.instance._lapTimesLeft[2];
                float currentSavedTimes = lap1time + lap2time + lap3time;

                //Checks wich laptimes combination is smaller and check if the saved times are not 0
                if (allTimesCombinedLeft < currentSavedTimes || currentSavedTimes == 0)
                {
                    //If a new best time is reached the laptimes will be saved to the PlayerPrefs
                    GameManager.instance.SetFloat(lap1left, GameManager.instance._lapTimesLeft[0]);
                    GameManager.instance.SetFloat(lap2left, GameManager.instance._lapTimesLeft[1]);
                    GameManager.instance.SetFloat(lap3left, GameManager.instance._lapTimesLeft[2]);

                    lap1time = GameManager.instance.GetFloat(lap1left);
                    lap2time = GameManager.instance.GetFloat(lap2left);
                    lap3time = GameManager.instance.GetFloat(lap3left);

                    //The UI will be updated for the laptimes wich are best
                    UIManager.instance.UpdateBestLapTimesLeft(lap1time, lap2time, lap3time);
                    allTimesCombinedLeft = 0;
                }
                else
                {
                    //if there is no new best reached time the ui will be updated with the old saved times
                    UIManager.instance.UpdateBestLapTimesLeft(lap1time, lap2time, lap3time);
                }
            }
            else if (!player.GetComponent<Car>()._isLeftCar)
            {
                Debug.Log("New highscore for right car!");
                //Gets all the saved laptimes
                float lap1time = GameManager.instance.GetFloat(lap1right);
                float lap2time = GameManager.instance.GetFloat(lap2right);
                float lap3time = GameManager.instance.GetFloat(lap3right);

                //Combines both all the current laptimes and the saved laptimes
                allTimesCombinedLeft = GameManager.instance._lapTimesRight[0] + GameManager.instance._lapTimesRight[1] + GameManager.instance._lapTimesRight[2];
                float currentSavedTimes = lap1time + lap2time + lap3time;

                //Checks wich laptimes combination is smaller and check if the saved times are not 0
                if (allTimesCombinedLeft < currentSavedTimes || currentSavedTimes == 0)
                {
                    //If a new best time is reached the laptimes will be saved to the PlayerPrefs
                    GameManager.instance.SetFloat(lap1right, GameManager.instance._lapTimesRight[0]);
                    GameManager.instance.SetFloat(lap2right, GameManager.instance._lapTimesRight[1]);
                    GameManager.instance.SetFloat(lap3right, GameManager.instance._lapTimesRight[2]);

                    lap1time = GameManager.instance.GetFloat(lap1right);
                    lap2time = GameManager.instance.GetFloat(lap2right);
                    lap3time = GameManager.instance.GetFloat(lap3right);

                    //The UI will be updated for the laptimes wich are best
                    UIManager.instance.UpdateBestLapTimesRight(lap1time, lap2time, lap3time);
                    allTimesCombinedLeft = 0;
                }
                else
                {
                    //if there is no new best reached time the ui will be updated with the old saved times
                    UIManager.instance.UpdateBestLapTimesRight(lap1time, lap2time, lap3time);
                }
            }
        }
    }

    private IEnumerator StartTheGame()
    {
        //the player can't drive until game has started
        GameManager.instance._canDrive = false;

        yield return new WaitForSeconds(3f);
        StartCoroutine(WaitASec());
        //Lets the GameManager know if the game has started to set the correct time scale and such
        GameManager.instance._gameIsActive = true;
        //The player can now drive
        GameManager.instance._canDrive = true;
        //Lets the timer know if it can start
        GameManager.instance._canTime = true;

        yield return null;
    }

    private IEnumerator WaitASec() //The first 4 seconds of the race the finishline collider will be turned off
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(4f);

        gameObject.GetComponent<BoxCollider>().enabled = true;

        yield return null;
    }

    private IEnumerator MissedLapTextTime() //Brings s small message up to let the player know they missed a checkpoint
    {
        UIManager.instance.MissedCheckpoint(true);

        yield return new WaitForSeconds(4f);

        UIManager.instance.MissedCheckpoint(false);

        yield return null;
    }

    private IEnumerator DNF()
    {
        yield return new WaitForSeconds(5f);
        _endScene.SetActive(true);
        Time.timeScale = 0;
        UIManager.instance.SetRaceLapTimesLeft();
        UIManager.instance.SetRaceLapTimesRight();
    }

    public void ResetTimes()
    {
        //clears the laptime List of the current race
        GameManager.instance._lapTimesRight.Clear();
        GameManager.instance._lapTimesLeft.Clear();
    }
}
