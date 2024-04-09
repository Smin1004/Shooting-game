using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Text score_box;
    public Text stage_box;
    public InputField p_name_box;
    public GameObject exit_button;

    public User user;

    int click;
    float score = 0;
    int stage;

    private void Start() {
        score = TempData.Instance.stageScore;
        stage = TempData.Instance.stageIndex + 1;
    }

    private void Update() {
        if(Input.anyKeyDown){
            UI(click);
        }
    }

    void UI(int i){
        if(click == 4) return;
        if(click == 3 && p_name_box.textComponent.text == "") return;

        switch(i){
            case 0 : score_box.text = string.Format("Score : {0:#,0}", score); score_box.gameObject.SetActive(true);
            break;

            case 1 : stage_box.text = string.Format("Stage : {0}", stage); stage_box.gameObject.SetActive(true);
            break;

            case 2 : p_name_box.gameObject.SetActive(true);
            break;

            case 3 : exit_button.SetActive(true);
            break;
        }

        click++;
    }

    public void Exit_Button(){
        user.datas.Add(new SaveData(p_name_box.text, score));
        user.datas = user.datas.OrderByDescending(item => item.user_score).ToList();
        user.datas = user.datas.GetRange(0, 3);
        var content = JsonUtility.ToJson(user);
        PlayerPrefs.SetString("save", content);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Title");
    }
}
