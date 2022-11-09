using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new distance achievement", menuName = "Achievement/DistanceAchievement"),]
public class DistanceAchievement : Achievement, IAchievement
{
    public int distanceNeededForAchievement;
}