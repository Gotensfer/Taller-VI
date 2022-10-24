using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new altitude achievement", menuName = "Achievement/AltitudeAchievement")]
public class AltitudeAchievement : Achievement, IAchievement
{
    public int altitudeNeededForAchievement;
}