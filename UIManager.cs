using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private TextMeshProUGUI _lapTextRight;
    [SerializeField] private TextMeshProUGUI _lapTextLeft;
    [SerializeField] private TextMeshProUGUI _lapTimeRight;
    [SerializeField] private TextMeshProUGUI _lapTimeLeft;
    [SerializeField] private TextMeshProUGUI _raceTimesRight;
    [SerializeField] private TextMeshProUGUI _raceTimesLeft;
    [SerializeField] private TextMeshProUGUI _bestLapTimesRight;
    [SerializeField] private TextMeshProUGUI _bestLapTimesLeft;
    [SerializeField] private TextMeshProUGUI _speedTextRight;
    [SerializeField] private TextMeshProUGUI _speedTextLeft;
    [SerializeField] private GameObject _missedCheckpoint;

    [SerializeField] private TextMeshProUGUI DNF;
    private void Awake()
    {
        instance = this;
    }

    public void UpdateLapUIRight(int lap) //The Laps counter
    {
        _lapTextRight.text = "Lap: " + lap + "/3";
    }
    public void UpdateLapUILeft(int lap) //The Laps counter
    {
        _lapTextLeft.text = "Lap: " + lap + "/3";
    }

    public void UpdateLapTimeRight(float p_lapTime) //The laptime in the UI
    {
        _lapTimeRight.text = "Lap Time:" + "\n" + p_lapTime.ToString();
    }
    public void UpdateLapTimeLeft(float p_lapTime) //The laptime in the UI
    {
        _lapTimeLeft.text = "Lap Time:" + "\n" + p_lapTime.ToString();
    }

    public void SetRaceLapTimesRight() //Sets all the laptimes that are currently reached
    {
        if (GameManager.instance._lapTimesRight.Count != 3)
        {
            _raceTimesRight.text = "DNF";

            float lap1time;
            float lap2time;
            float lap3time;
            if (GameManager.instance._spain)
            {
                lap1time = GameManager.instance.GetFloat(StartFinish.instance.lap1rightspain);
                lap2time = GameManager.instance.GetFloat(StartFinish.instance.lap2rightspain);
                lap3time = GameManager.instance.GetFloat(StartFinish.instance.lap3rightspain);
            }
            else
            {
                lap1time = GameManager.instance.GetFloat(StartFinish.instance.lap1right);
                lap2time = GameManager.instance.GetFloat(StartFinish.instance.lap2right);
                lap3time = GameManager.instance.GetFloat(StartFinish.instance.lap3right);
            }

            UpdateBestLapTimesRight(lap1time, lap2time, lap3time);
        }
        else
        {
            _raceTimesRight.text = "Lap 1: " + GameManager.instance._lapTimesRight[0].ToString() + "\n" +
                              "Lap 2: " + GameManager.instance._lapTimesRight[1].ToString() + "\n" +
                              "Lap 3: " + GameManager.instance._lapTimesRight[2].ToString();
        }
    }
    public void SetRaceLapTimesLeft() //Sets all the laptimes that are currently reached
    {
        if (GameManager.instance._lapTimesLeft.Count != 3)
        {
            _raceTimesLeft.text = "DNF";


            float lap3time;
            float lap1time;
            float lap2time;
            if (GameManager.instance._spain)
            {
                lap1time = GameManager.instance.GetFloat(StartFinish.instance.lap1leftspain);
                lap2time = GameManager.instance.GetFloat(StartFinish.instance.lap2leftspain);
                lap3time = GameManager.instance.GetFloat(StartFinish.instance.lap3leftspain);
            }
            else
            {
                lap1time = GameManager.instance.GetFloat(StartFinish.instance.lap1left);
                lap2time = GameManager.instance.GetFloat(StartFinish.instance.lap2left);
                lap3time = GameManager.instance.GetFloat(StartFinish.instance.lap3left);
            }

            UpdateBestLapTimesLeft(lap1time, lap2time, lap3time);
        }
        else
        {
            _raceTimesLeft.text = "Lap 1: " + GameManager.instance._lapTimesLeft[0].ToString() + "\n" +
                              "Lap 2: " + GameManager.instance._lapTimesLeft[1].ToString() + "\n" +
                              "Lap 3: " + GameManager.instance._lapTimesLeft[2].ToString();
        }
    }

    public void SpeedUpdateRight(float speed)
    {
        //int roundedUpSpeed = Mathf.RoundToInt(speed);
        _speedTextRight.text = (int)speed * 3.6f + " KM/H";
    }
    public void SpeedUpdateLeft(float speed)
    {
        //int roundedUpSpeed = Mathf.RoundToInt(speed);
        _speedTextLeft.text = (int)speed * 3.6f + " KM/H";
    }

    /// <summary>
    /// The function where the best laptimes will be shown
    /// </summary>
    /// <param name="lap1"></the laptime for the first lap>
    /// <param name="lap2"></the laptime for the second lap>
    /// <param name="lap3"></the laptime for the thrid lap>
    public void UpdateBestLapTimesRight(float lap1, float lap2, float lap3)
    {
        _bestLapTimesRight.text = "Lap 1: " + lap1.ToString() + "\n" +
                             "Lap 2: " + lap2.ToString() + "\n" +
                             "Lap 3: " + lap3.ToString();
    }
    public void UpdateBestLapTimesLeft(float lap1, float lap2, float lap3)
    {
        _bestLapTimesLeft.text = "Lap 1: " + lap1.ToString() + "\n" +
                             "Lap 2: " + lap2.ToString() + "\n" +
                             "Lap 3: " + lap3.ToString();
    }

    public void MissedCheckpoint(bool active)
    {
        _missedCheckpoint.SetActive(active);
    } //This function will set the message that you missed a checkpoint

    public void didNotFinish()
    {
        DNF.enabled = true;
    }

}
