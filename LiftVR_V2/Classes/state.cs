using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class state
{
    public enum animationState
    {
        walking, idle, talking
    }

    public enum floor
    {
        Basement = 0, Lobby = 1, Office = 2, Ballroom = 3, Four = 4, Five = 5, Restaurant = 6, Empty = 7
    }

    public enum group
    {
        GroupA, GroupB, GroupC, GroupD
    }

    public enum dayCycle
    {
        Morning = 0, Midday = 1, Evening = 2, Night = 3
    }

    public enum foodType
    {
        Meat = 0, Soup = 1, Dessert = 2, Random = 3
    }
}
