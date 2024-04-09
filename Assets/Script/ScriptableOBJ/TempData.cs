using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Temp", menuName ="Game/Temp", order = int.MinValue), SerializeField]
public class TempData : ScriptableObject
{
    private static TempData _instance = null;
    public static TempData Instance => _instance;

    public float stageScore;
    public int weaponLevel = 0; // 0 ~ 3

    public int stageIndex;

    public void InitTempData()
    {
        _instance = this;
    }
}
