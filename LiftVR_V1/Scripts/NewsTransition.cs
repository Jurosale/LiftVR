using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsTransition : MonoBehaviour {

    public Material news;
    public Texture boss;
    public Texture artist;
    public Texture serverH;
    public Texture serverN;
    public Texture serverA;
    public Texture adultressH;
    public Texture adultressN;
    public Texture adultressA;
    public Texture businessH;
    public Texture businessN;
    public Texture businessA;

    private GameObject objMood;

    // Use this for initialization
    void Start () {
        objMood = GameObject.FindGameObjectWithTag("HotelManager");
    }
	
	// Update is called once per frame
	public void DAY1() {
        news.mainTexture = boss;
    }

    public void DAY2()
    {
        news.mainTexture = artist;
    }

    public void DAY3()
    {
        if (objMood.GetComponent<AIInfo>().serverMood > 3) { news.mainTexture = serverH; }
        else if (objMood.GetComponent<AIInfo>().serverMood < -3) { news.mainTexture = serverA; }
        else { news.mainTexture = serverN; }
    }

    public void DAY4()
    {
        if (objMood.GetComponent<AIInfo>().adultMood > 3) { news.mainTexture = adultressH; }
        else if (objMood.GetComponent<AIInfo>().adultMood < -3) { news.mainTexture = adultressA; }
        else { news.mainTexture = adultressN; }
    }

    public void DAY5()
    {
        if (objMood.GetComponent<AIInfo>().businessMood > 3) { news.mainTexture = businessH; }
        else if (objMood.GetComponent<AIInfo>().businessMood < -3) { news.mainTexture = businessA; }
        else { news.mainTexture = businessN; }
    }
}
