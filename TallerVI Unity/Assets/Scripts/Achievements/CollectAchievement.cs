using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameCollectType
{
    Chilli = 0,
    Pidgeon = 1,
    Rocket = 2
}

[CreateAssetMenu(fileName = "new collect achievement", menuName = "Achievement/CollectAchievement")]
public class CollectAchievement : Achievement, IAchievement
{
    public GameCollectType gameCollectType;
    public int collectsNeededForAchievement;
}