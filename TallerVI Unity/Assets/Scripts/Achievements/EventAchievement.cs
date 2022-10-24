using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameEventType
{
    Launch = 0,
    Bounce = 1,
    Crash = 2,
}

[CreateAssetMenu(fileName = "new event achievement", menuName = "Achievement/EventAchievement")]
public class EventAchievement : Achievement, IAchievement
{
    public GameEventType gameEventType;
    public int timesNeededForAchievement;
}