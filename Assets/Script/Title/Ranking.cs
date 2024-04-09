using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    public List<Text> ranking = new List<Text>();
    int j =0;

    public User user;

    private void Start() {
        if(PlayerPrefs.HasKey("save")){
            var content = PlayerPrefs.GetString("save");
            JsonUtility.FromJsonOverwrite(content, user);
        }
        for (int i = 0; i < Mathf.Min(user.datas.Count, 3); i++)
        {
            var content = user.datas[i];
            ranking[j].text = string.Format(content.user_name);
            j++;
            ranking[j].text = string.Format("Score : {0:#,0}", content.user_score);
            j++;
        }
    }
}
