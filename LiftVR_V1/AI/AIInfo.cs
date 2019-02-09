using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class AIInfo : MonoBehaviour {

    private Regex touristRegex = new Regex(@"Tourist");
    private Regex bossRegex = new Regex(@"Boss");
    private Regex businessRegex = new Regex(@"Business");
    private Regex adultressRegex = new Regex(@"Adultress");
    private Regex serverRegex = new Regex(@"Server");
    private Regex artistRegex = new Regex(@"Artist");

    [SerializeField]
    public int bossMood;
    [SerializeField]
    public int businessMood;
    [SerializeField]
    public int touristMood;
    [SerializeField]
    public int adultMood;
    [SerializeField]
    public int serverMood;
    [SerializeField]
    public int artistMood;

    private bool divorce = false;
    [SerializeField]
    private int businessPick; //0 = server 1 = artist 2 = other
    
    public void setMood(string name, int mood) {
        if (touristRegex.IsMatch(name)) touristMood = mood;
        else if (bossRegex.IsMatch(name)) bossMood = mood;
        else if (businessRegex.IsMatch(name)) businessMood = mood;
        else if (adultressRegex.IsMatch(name)) adultMood = mood;
        else if (serverRegex.IsMatch(name)) serverMood = mood;
        else if (artistRegex.IsMatch(name)) artistMood = mood;
        else throw new System.ArgumentOutOfRangeException("PATRON NOT FOUND, TRIED TO PASS: " + name);
    }

    public int getMood(string name) {
        if (touristRegex.IsMatch(name)) return touristMood;
        else if (bossRegex.IsMatch(name)) return bossMood;
        else if (businessRegex.IsMatch(name)) return businessMood;
        else if (adultressRegex.IsMatch(name)) return adultMood;
        else if (serverRegex.IsMatch(name)) return serverMood;
        else if (artistRegex.IsMatch(name)) return artistMood;
        else throw new System.ArgumentOutOfRangeException("PATRON NOT FOUND, TRIED TO PASS: " + name);
    }

    public void flagDivorce() {
        divorce = true;
    }

    public bool getDivorce() {
        return divorce;
    }

    public void setBusinessPick(int i) {
        businessPick = i;
    }

    public int getBusinessPick() {
        return businessPick;
    }
}
