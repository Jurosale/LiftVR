using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class musicController : MonoBehaviour {

    [Range(0f, 1f)]
    public float elevatorMusicVolume;
    [Range(0f, 1f)]
    public float themeMusicVolume;

    [Space]

    public AudioClip elevatorMusic;
    public AudioClip mainTheme;
    public AudioClip bossTheme;
    public AudioClip businessTheme;
    public AudioClip touristTheme;
    public AudioClip serverTheme;
    public AudioClip adultressTheme;
    public AudioClip artistTheme;
	
	// Update is called once per frame
	void Update () {
		
	}

    //Called from other Scripts when patron enter
    //0 is main theme, 1 is bossTheme, 2 is businessTheme, 3 is touristTheme
    public void playCharacterTheme(int character) {
        var newClip = mainTheme;
        if(character == 0) { newClip = mainTheme; }
        if(character == 1) { newClip = bossTheme; }
        if(character == 2) { newClip = businessTheme; }
        if(character == 3) { newClip = touristTheme; }
        if (character == 4) { newClip = serverTheme; }
        if (character == 5) { newClip = adultressTheme; }
        if (character == 6) { newClip = artistTheme; }

        GetComponent<AudioSource>().clip = newClip;
        GetComponent<AudioSource>().volume = themeMusicVolume;
        GetComponent<AudioSource>().Play();

        //StartCoroutine(ExecuteAfterTime(newClip.length));
    }

    public void characterExit() {
        GetComponent<AudioSource>().clip = elevatorMusic;
        GetComponent<AudioSource>().volume = elevatorMusicVolume;
        GetComponent<AudioSource>().Play();
    }
}
