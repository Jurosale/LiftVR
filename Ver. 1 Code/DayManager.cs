using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    [Header("Globals")]
    public bool patronPresent = false;                          //If true there is a patron in the elevator

    [Header("Patrons")]
    public string[] day1;
    public string[] day2;
    public string[] day3;
    public string[] day4;
    public string[] day5;

    //In the days array 
    private string[][] days = new string[5][];

    [Header("Timeline")]
    [Range(1, 5)]
    public int day = 1;
    [Range(1, 4)]
    [Tooltip("The number patron the game will start with. 1st Patron is 1")]
    public int patronNumber = 1;

    [Header("Day Reset")]
    public int fadeToBlackTime;
    public int timeInBlack;
    public int unFadeTime;
    private GameObject elevatorManager;
    public doorInteraction liftableDoor;
    private GameObject leverRotator;
    public Material skybox;

    public string dayResetSound;

    private Patrons patron = new Patrons();

    private AsyncOperation async;

    private GameObject objNews;

    // Use this for initialization
    void Start()
    {
        elevatorManager = GameObject.FindGameObjectWithTag("ElevatorManager");
        leverRotator = GameObject.FindGameObjectWithTag("lever");
        objNews = GameObject.FindGameObjectWithTag("News");

        skybox.SetFloat("_Exposure", 2.4f);
        skybox.SetFloat("_AtmosphereThickness", 0.3f);

        days[0] = day1; days[1] = day2; days[2] = day3; days[3] = day4; days[4] = day5;
        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, 10);

        patronNumber -= 1;
        objNews.GetComponent<NewsTransition>().DAY1();   
        nextPatron();
    }

    //Spawns the next patron in the timeline, if at the end of a day move to next day
    public void nextPatron()
    {
        if (patronNumber < days[day - 1].Length)
        {

            patronNumber += 1;

            //Updates skybox to simulate different parts of the day
            if (patronNumber == 2) { skybox.SetFloat("_AtmosphereThickness", 0.7f); }
            if (patronNumber == 3) { skybox.SetFloat("_AtmosphereThickness", 1.0f); }
            if (patronNumber == 4) { skybox.SetFloat("_AtmosphereThickness", 1.4f); }

            var patronObject = patron.fetchPatron(days[day - 1][patronNumber - 1]);
            var patronPrefab = patronObject.prefab;
            var startFloor = patronObject.startFloor;

            Quaternion basePatronRotation = patronPrefab.transform.rotation;

            var newPatron = Instantiate(patronPrefab, GetComponent<FloorManager>().fetchFloorWaypoint(startFloor).transform.position, basePatronRotation);

            //sets script
            patron.configPatron(ref newPatron, days[day - 1][patronNumber - 1]);

            newPatron.transform.parent = GetComponent<FloorManager>().floors[startFloor].transform;

            //Set the light on the elevator
            leverRotator.GetComponent<patronWaiting>().lightOn(startFloor);

            //floorPanel.GetComponent<FloorsPanel>().lightOn(startFloor);

            //GetComponent<FloorManager>().patrons[startFloor] += 1;
        }
        else
        {
            nextDay();
        }
    }

    public void nextDay()
    {
        if (day < 5)
        {
            patronNumber = 0;
            day += 1;

            SteamVR_Fade.Start(Color.clear, 0);
            SteamVR_Fade.Start(Color.black, fadeToBlackTime);

            StartCoroutine(ExecuteAfterTime(timeInBlack));
        }
        //We've reached the end of the game timeline
        else
        {
            StartCoroutine(toCredits());
        }
    }


    public void dayReset()
    {
        //changes the newspaper on every new day
        if (day == 2) { objNews.GetComponent<NewsTransition>().DAY2(); }
        if (day == 3) { objNews.GetComponent<NewsTransition>().DAY3(); }
        if (day == 4) { objNews.GetComponent<NewsTransition>().DAY4(); }
        if (day == 5) { objNews.GetComponent<NewsTransition>().DAY5(); }

        //Close the elevator door
        //Reset elevator position etc.
        leverRotator.GetComponent<LeverRotation>().resetLever();
        elevatorManager.GetComponent<ElevatorMovement>().arriveAtFloor(0);
        liftableDoor.closeDoor();
        //Set skybox to early morning
        skybox.SetFloat("_AtmosphereThickness", 0.3f);

        nextPatron();

        SteamVR_Fade.Start(Color.black, 0);
        SteamVR_Fade.Start(Color.clear, unFadeTime);
    }

    IEnumerator ExecuteAfterTime(float time) {

        yield return new WaitForSeconds(5f);

        dayResetSound.PlaySound();

        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        dayReset();
    }

    IEnumerator toCredits() {

        async = SceneManager.LoadSceneAsync("credits");
        async.allowSceneActivation = false;

        SteamVR_Fade.Start(Color.clear, 0);
        SteamVR_Fade.Start(Color.black, 5f);

        yield return new WaitForSeconds(5f);

        async.allowSceneActivation = true;
    }
}
