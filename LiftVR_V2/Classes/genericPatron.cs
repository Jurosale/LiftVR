using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class genericPatron : MonoBehaviour
{
    private void Start()
    {
        onSpawn();
    }

    private void Update()
    {
        onUpdate();
    }

    //The Following public functions which are called from outside the patron
    public void enterElevator()
    {
        onEnterElevator();

        onLeaveElevator();
    }

    public void leaveElevator()
    {

    }

    //The following virtual functions which are controlled by specific children of the patron
    public virtual void onSpawn()
    {

    }

    public virtual void onUpdate()
    {

    }

    public virtual void onEnterElevator()
    {

    }

    public virtual void onLeaveElevator()
    {

    }
}
