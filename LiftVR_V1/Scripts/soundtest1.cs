using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundtest1 : MonoBehaviour {

    public SoundGroup loopedSound;

    public string loopsound;
    public string loopsound2;
    public bool playing = true;
    bool playagain = true;
    // Update is called once per frame
    void Update() {
        /*//testing purposes only
        if (playing)
        {
        //starts playing two seperate looping sounds with different sound group prefabs
            loopsound2.PlaySound(transform.position);
            loopsound.PlaySound(transform.position);
            playing = false;
        }
        if (Input.GetKey("m")) {
            //searches for playing the sound by name (I wish there was another way to do this, but this is the best that I could come up with) and then calls
            //pingSound (which essentially kills the loop)
            GameObject myObject = GameObject.Find("_SFX_loopsoundprefab");
            myObject.GetComponent<SoundGroup>().pingSound();
            //Things that did not work (ignore this)
            //loopedSound.pingSound();
            //SoundManager.RecycleSoundToPool(loopedSound);
            //SoundGroup SndToPlay = null;
            //SoundManager.instance.SoundGroupsDictionary.TryGetValue(loopedSound, out SndToPlay);
            //SoundManager.RecycleSoundToPool(SndToPlay);
            //GameObject myObject = GameObject.Find("loopsoundprefab");
            //myObject.GetComponent<SoundGroup>().pingSound();
            //SoundManager.instance.currentMusic.ForceStop();
            
        }
        //starts the sound again (plays the looped sound again, to test if it is still working)
        if (Input.GetKey("n") && (playagain)){
            loopsound.PlaySound(transform.position);
            playagain = false;
        }
    */
    }
}

/* README
 * I finally managed to get this "working", even if it is kinda weird. Basically, in any sound group there is a built in coroutine (WaitForStopPlaying()) that checks 
 * if the sound has finished playing. If it has finsihed, then it moves it from the sounds playing pool into the sounds pool. (RecycleSoundToPool()). Obviously looping 
 * sounds never finish, so I have added a trigger within the coroutine that activates RecycleSoundToPool whenever the public variable attached to the prefab ceaseloop is true. 
 * This also deletes the sound obj created. This gets a little weird because SoundsPlaying is creating a new soundprefab with a modified name (prefix of _SFX_), so the best way 
 * I have gotten this to function is to search for the created prefab directly using Find.
 * 
 * To make it work:
 * in the sound prefab you want to loop, click the loop checkbox
 * create a new variable in the script that plays the looping sound of type SoundGroup. drag the looping sound prefab into this field in the inspector window
 * 
 * to start the looping sound, use the standard play sound line (with the same implementation as before):
 * stringnameofsound.PlaySound(transform.position);
 * 
 * when you want to make the sound stop, use this code:
 * GameObject myObject = GameObject.Find("_SFX_loopsoundprefab");
 * myObject.GetComponent<SoundGroup>().pingSound();
 * 
 * GameObject.Find is looking for the EXACT name of the created obj in the SoundsPlaying folder. The exact name is always _SFX_nameofsoundprefab
 * GameObjectFind is an inefficient function, so be sure to only call it when necessary, and only once per desired sound stop
 * if anyone needs more detailed instructions, just let me know. I could also demonstrate how it works over skype screen share or in person
 * 
 * Sidenote: This procedure is not necessary for sounds with the Music tag. Music automatically replaces the currently playing music when played. 
 * However that also means that only one music prefab can be playing at once
 */
