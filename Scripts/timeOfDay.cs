using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeOfDay : MonoBehaviour {

    [ReadOnly]
    public state.dayCycle phase = state.dayCycle.Morning;

    //Morning, Midday, Evening and Night
    public Material[] skyboxes;

    public state.dayCycle fetchTime()
    {
        return phase;
    }

    public void advanceTime()
    {
        var eValue = (int)phase;
        //If we are on the last value of the enum
        if (eValue + 1 == System.Enum.GetNames(typeof(state.floor)).Length)
        {
            //Reset to start
            setTime((state.dayCycle)0);
        }
        else
        {
            setTime((state.dayCycle)eValue + 1);
        }
    }

    public void setTime(state.dayCycle target)
    {
        phase = target;
        RenderSettings.skybox = skyboxes[(int)target];
    }
}
