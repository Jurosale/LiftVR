using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class Patrons {

    public class Patron {
        public GameObject prefab;
        public int startFloor;

        public Patron(GameObject preF, int sFloor) {
            prefab = preF;
            startFloor = sFloor;
        }
    }

    public Patron fetchPatron(string patronName) {

        GameObject prefab;

        Regex touristRegex = new Regex(@"Tourist");
        Regex bossRegex = new Regex(@"Boss");
        Regex businessRegex = new Regex(@"Business");
        Regex adultressRegex = new Regex(@"Adultress");
        Regex serverRegex = new Regex(@"Server");
        Regex artistRegex = new Regex(@"Artist");
        Regex date1Regex = new Regex(@"Date1");
        Regex date2Regex = new Regex(@"Date2");

        GameObject touristPatron = Resources.Load("Patrons/Tourist1") as GameObject;
        GameObject bossPatron = Resources.Load("Patrons/Boss1") as GameObject;
        GameObject businessPatron = Resources.Load("Patrons/Business1") as GameObject;
        GameObject adultressPatron = Resources.Load("Patrons/Adultress1") as GameObject;
        GameObject serverPatron = Resources.Load("Patrons/Server1") as GameObject;
        GameObject artistPatron = Resources.Load("Patrons/Artist1") as GameObject;
        GameObject date1Patron = Resources.Load("Patrons/Date1") as GameObject;
        GameObject date2Patron = Resources.Load("Patrons/Date2") as GameObject;

        if (touristRegex.IsMatch(patronName)) prefab = touristPatron;
        else if (bossRegex.IsMatch(patronName)) prefab = bossPatron;
        else if (businessRegex.IsMatch(patronName)) prefab = businessPatron;
        else if (adultressRegex.IsMatch(patronName)) prefab = adultressPatron;
        else if (serverRegex.IsMatch(patronName)) prefab = serverPatron;
        else if (artistRegex.IsMatch(patronName)) prefab = artistPatron;
        else if (date1Regex.IsMatch(patronName)) prefab = date1Patron;
        else if (date2Regex.IsMatch(patronName)) prefab = date2Patron;
        else throw new System.ArgumentOutOfRangeException("PREFAB NOT FOUND, TRIED TO PASS: " + patronName);

        if (patronName == "Boss1") return new Patron(prefab, 0);

        if (patronName == "Boss2") return new Patron(prefab, 1);

        if (patronName == "Boss3") return new Patron(prefab, 6);

        if (patronName == "Business1") return new Patron(prefab, 1);

        if (patronName == "Business2") return new Patron(prefab, 3);

        if (patronName == "Business3") return new Patron(prefab, 3);

        if (patronName == "Business4") return new Patron(prefab, 1);

        if (patronName == "Tourist1") return new Patron(prefab, 1);

        if (patronName == "Tourist2") return new Patron(prefab, 5);

        if (patronName == "Tourist3") return new Patron(prefab, 5);

        if (patronName == "Adultress1") {
            prefab.transform.Find("Date1").gameObject.SetActive(true);
            prefab.transform.Find("Date2").gameObject.SetActive(false);
            return new Patron(prefab, 1);
        }

        if (patronName == "Adultress2") {
            prefab.transform.Find("Date2").gameObject.SetActive(true);
            prefab.transform.Find("Date1").gameObject.SetActive(false);
            return new Patron(prefab, 1);
        }

        if (patronName == "Adultress3") throw new System.ArgumentException("NOT YET IMPLEMENTED");

        if (patronName == "Artist1") return new Patron(prefab, 2);

        if (patronName == "Artist2") return new Patron(prefab, 5);

        if (patronName == "Artist3") return new Patron(prefab, 4);

        if (patronName == "Server1") return new Patron(prefab, 6);

        if (patronName == "Server2") return new Patron(prefab, 6);

        if (patronName == "Server3") return new Patron(prefab, 6);

        if (patronName == "Server4") return new Patron(prefab, 6);

        throw new System.ArgumentOutOfRangeException("STARTING FLOOR NOT FOUND, TRIED TO PASS: " + patronName);
    }

    public void configPatron(ref GameObject patronObject, string patronName)
    {

        if (patronName == "Boss1")
        {
            patronObject.AddComponent<BossDay1AI>();

            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().dayName = "Day1";
        }

        else if (patronName == "Boss2")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("2.2Boss.json");
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().dayName = "Day2";
        }

        else if (patronName == "Boss3")
        {
            patronObject.AddComponent<GenericAIv2>();
            //patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossN.json");
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            //Updated to include different moods
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossH.json");
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossA.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("5.4BossN.json");
            patronObject.GetComponent<PatronAudio>().patronName = "Boss";
            patronObject.GetComponent<PatronAudio>().dayName = "Day5";
        }

        else if (patronName == "Business1")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("2.1Businessman.json");

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day2";
        }

        else if (patronName == "Business2")
        {
            patronObject.AddComponent<GenericAIv2>();
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("3.2BusinessmanH.json");
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename("3.2BusinessmanA.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("3.2BusinessmanN.json");

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day3";
        }

        else if (patronName == "Business3")
        {
            //Hard coded interaction
            patronObject.AddComponent<BusinessDay4AI>();
            patronObject.GetComponent<BusinessDay4AI>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day4";
        }

        else if (patronName == "Business4") {
            patronObject.AddComponent<GenericAIv2>();
            AIInfo info = (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo);
            short mood = (short)info.getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (mood > 3) {
                int index = info.getBusinessPick();
                if (index == 0) patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanH2.json");
                else if (index == 1) patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanH1.json");
                else patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanH3.json");
            }
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanA.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("5.1BusinessmanN.json");

            patronObject.GetComponent<PatronAudio>().patronName = "BusinessMan";
            patronObject.GetComponent<PatronAudio>().dayName = "Day5";
        }

        else if (patronName == "Tourist1")
        {
            patronObject.AddComponent<TouristDay1AI>();

            patronObject.GetComponent<PatronAudio>().patronName = "Tourist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day1";
        }

        else if (patronName == "Tourist2")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("3.1Tourist.json");
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Tourist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day3";
        }

        else if (patronName == "Tourist3")
        {
            patronObject.AddComponent<TouristDay4AI>();
            patronObject.GetComponent<TouristDay4AI>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Tourist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day4";
        }

        else if (patronName == "Adultress1")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("1.3Adultress.json");
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Adultress";
            patronObject.GetComponent<PatronAudio>().dayName = "Day1";
        }

        else if (patronName == "Adultress2")
        {
            patronObject.AddComponent<GenericAIv2>();
            int mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood((short) mood);
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("3.3AdultressH.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("3.3AdultressAN.json");

            patronObject.GetComponent<PatronAudio>().patronName = "Adultress";
            patronObject.GetComponent<PatronAudio>().dayName = "Day3";
        }

        else if (patronName == "Adultress3")
        {
            throw new System.ArgumentException("NOT YET IMPLEMENTED");
        }

        else if (patronName == "Artist1")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("2.3Artist.json");
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Artist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day2";
        }

        else if (patronName == "Artist2")
        {
            patronObject.AddComponent<GenericAIv2>();
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("4.1ArtistH.json");
            else patronObject.GetComponent<GenericAIv2>().setFilename("4.1ArtistAN.json");

            patronObject.GetComponent<PatronAudio>().patronName = "Artist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day4";
        }

        else if (patronName == "Artist3")
        {
            patronObject.AddComponent<GenericAIv2>();
            AIInfo info = (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo);
            short mood = (short)info.getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (info.getBusinessPick() == 1) {
                if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("5.2Artist3NEWL.json");
                else patronObject.GetComponent<GenericAIv2>().setFilename("5.2Artist3PERM.json");
            } else {
                if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("5.2Artist3INTO.json");
                else patronObject.GetComponent<GenericAIv2>().setFilename("5.2Artist3FALL.json");
            }

            patronObject.GetComponent<PatronAudio>().patronName = "Artist";
            patronObject.GetComponent<PatronAudio>().dayName = "Day5";
        }

        else if (patronName == "Server1")
        {
            patronObject.AddComponent<GenericAIv2>();
            patronObject.GetComponent<GenericAIv2>().setFilename("1.4Server.json");
            patronObject.GetComponent<GenericAIv2>().setMood((short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName));

            patronObject.GetComponent<PatronAudio>().patronName = "Server";
            patronObject.GetComponent<PatronAudio>().dayName = "Day1";
        }

        else if (patronName == "Server2")
        {
            patronObject.AddComponent<GenericAIv2>();
            string[] s = { "2.4ServerH.json", "2.4ServerN.json", "2.4ServerA.json" };
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename(s[0]);
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename(s[2]);
            else patronObject.GetComponent<GenericAIv2>().setFilename(s[1]);

            patronObject.GetComponent<PatronAudio>().patronName = "Server";
            patronObject.GetComponent<PatronAudio>().dayName = "Day2";
        }

        else if (patronName == "Server3")
        {
            patronObject.AddComponent<GenericAIv2>();
            string[] s = { "3.4ServerH.json", "3.4ServerN.json", "3.4ServerA.json" };
            short mood = (short)(GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo).getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename(s[0]);
            else if (mood < -3) patronObject.GetComponent<GenericAIv2>().setFilename(s[2]);
            else patronObject.GetComponent<GenericAIv2>().setFilename(s[1]);

            patronObject.GetComponent<PatronAudio>().patronName = "Server";
            patronObject.GetComponent<PatronAudio>().dayName = "Day3";
        }

        else if (patronName == "Server4")
        {
            patronObject.AddComponent<GenericAIv2>();
            AIInfo info = (GameObject.FindWithTag("HotelManager").GetComponent(typeof(AIInfo)) as AIInfo);
            short mood = (short)info.getMood(patronName);
            patronObject.GetComponent<GenericAIv2>().setMood(mood);
            if (info.getBusinessPick() == 0) {
                if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("4.4Server1H.json");
                else patronObject.GetComponent<GenericAIv2>().setFilename("4.4Server1AN.json");
            } else {
                if (mood > 3) patronObject.GetComponent<GenericAIv2>().setFilename("4.4Server2H.json");
                else patronObject.GetComponent<GenericAIv2>().setFilename("4.4Server2AN.json");
            }

            patronObject.GetComponent<PatronAudio>().patronName = "Server";
            patronObject.GetComponent<PatronAudio>().dayName = "Day4";
        }

        else
        {
            throw new System.ArgumentException("Patron Name not found: " + patronName);
        }
    }
}
