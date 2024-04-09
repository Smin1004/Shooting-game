using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "User", menuName = "Game/User", order = int.MinValue), Serializable]
public class User : ScriptableObject
{
    public List<SaveData> datas = new List<SaveData>();
}

[Serializable]
public class SaveData
{
    public SaveData(string name, float score){
        user_name = name;
        user_score = score;
    }

    public string user_name;
    public float user_score;
}