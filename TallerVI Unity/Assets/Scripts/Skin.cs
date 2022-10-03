using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "Skin")]
public class Skin : ScriptableObject
{
    public SkinID skinID;
    public string skinName;
    public int price;
}
