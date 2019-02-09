using UnityEngine;
using System.Collections;
//animation is currently scaled based on the SIZE (not positioning) of the door, if we choose to re-size the models, the animation may need to be modified
//the door will most likely need to be thinner/moved in order to not clip with the lever wall, this should not interfere with the animation
//*we also need a trigger that prevents the player from moving the elevator while the door is open
public class slidingDoor2 : MonoBehaviour
{

    Animator slidingDoor;
    bool doorOpen;
    public string bellSFX;
    public string doorCloseSFX;
    public string doorOpenSFX;
    void Start()
    {
        doorOpen = false;
        slidingDoor = GetComponent<Animator>();
    }

    public void openSlidingDoor()
    {
        if (!doorOpen)
        {
            doorOpen = true;
            DoorControl("open");
            doorOpenSFX.PlaySound(transform.position);
            bellSFX.PlaySound(transform.position);
        }
    }

    public void closeSlidingDoor()
    {
        if (doorOpen)
        {
            doorOpen = false;
            DoorControl("closed");
            doorCloseSFX.PlaySound(transform.position);
            bellSFX.PlaySound(transform.position);
        }
    }

    void DoorControl(string direction)
    {
        slidingDoor.SetTrigger(direction);
    }

    void Update()
    {
        /*
        if (Input.GetKey("l"))//open
            openSlidingDoor();
        if (Input.GetKey("k"))//close
            closeSlidingDoor();
        */

        /*example of playing a sound. doesnt work well with update calling the sound multiple times (and im just trying to show proof of concept) 
         * in the code where it will actually be used, it should only called once per sound. import sound files into the AudioClips file
         * to add additional sounds, copy an existing prefab within SoundGroups and modify the elements by including the sound pool you want to play
         * pitch is optional. make sure spatial blend is set to 3d. sound prefabs must remain in SoundGroups folder to be located by the script
         * create a variable for the object creating the sound <doorSFX> and in the inspector set that field to the same name as the prefab you want from SoundGroups
         * some additional sounds are in the googledrive*/

       
            
    }//----------------------------------------

}