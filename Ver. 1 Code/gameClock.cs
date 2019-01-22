using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameClock : MonoBehaviour {

    public Text timeLeft;
    private double timeFunc;
    public double GameTimer;
    private double dayLength;
    private double timer;
    private int mins;
    private int sec;
    private string displayMin;
    private string displaySec;
    public Text dayNum;
    private int dayCount;
    private bool isDay;
    private bool displayed;

    private bool isYes;
    private bool isNo;
    private bool isContinue;
    private bool isConfused;
    private bool isRude;
    private bool isSalute;
    private bool isOut;

    // Use this for initialization
    void Start () {
        displayed = false;
        dayLength = GameTimer; // set the desired value of time per day in Manager/GameClcok script/ "Game Timer" field
        timer = 0.0;
        dayCount = 1;
        isDay = true;
        isYes = false;
        isNo = false;
        isConfused = false;
        isContinue = false;
        isRude = false;
        isSalute = false;
        isOut = false;
        //dayNum.text = "Day: " + dayCount.ToString();
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetKeyDown(KeyCode.M) && isOut == false)
        {
            dayNum.text += "Out\n";
            isOut = true;
        }
        if (Input.GetKeyDown(KeyCode.Z) && isYes == false)
        {
            dayNum.text += "Yes\n";
            isYes = true;
        }
        if (Input.GetKeyDown(KeyCode.X) && isNo == false)
        {
            dayNum.text += "No\n";
            isNo = true;
        }
        if (Input.GetKeyDown(KeyCode.C) && isContinue == false)
        {
            dayNum.text += "Continue\n";
            isContinue = true;
        }
        if (Input.GetKeyDown(KeyCode.V) && isConfused == false)
        {
            dayNum.text += "Confused\n";
            isConfused = true;
        }
        if (Input.GetKeyDown(KeyCode.B) && isRude == false)
        {
            dayNum.text += "Rude\n";
            isRude = true;
        }
        if (Input.GetKeyDown(KeyCode.N) && isSalute == false)
        {
            dayNum.text += "Salute\n";
            isSalute = true;
        }
        if (isDay) { 
            timeFunc = dayLength - timer; // makes timeFunc count down from dayLength in realtime
            displayTimeCorrectly();
            timer = Time.timeSinceLevelLoad;
            if (timeFunc <= 0) {
                isDay = false;
                dayCount += 1;
                //dayNum.text = dayCount.ToString();
            } 
        }
	}

    // this was created to ensure we don't get time displays like '2:5' when it should be '2:05'
    void displayTimeCorrectly () {
        mins = (int)timeFunc / 60;
        sec = (int)timeFunc % 60;

        if (mins == 0) {
            displayMin = "";
        } else { displayMin = mins.ToString("f0"); }
        
        if (sec < 10) {
            displaySec = "0" + sec.ToString("f0");
        } else { displaySec = sec.ToString("f0"); }

        if (displayed == true) {timeLeft.text = displayMin + " : " + displaySec;}
        else { timeLeft.text = ""; }
    }
}
