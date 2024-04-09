using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title_Button : MonoBehaviour
{
    public GameObject Window_HowToPlay;
    public GameObject Window_Ranking;
    public GameObject Window_Exit;

    public void GameStart()
    {
        TempData.Instance.stageIndex = 0;
        TempData.Instance.weaponLevel = 0;
        TempData.Instance.stageScore = 0;

        SceneManager.LoadScene("InGame");
    }

    public void HowToPlay()
    {
        Window_HowToPlay.SetActive(true);
    }

    public void Ranking()
    {
        Window_Ranking.SetActive(true);
    }

    public void Exit()
    {
        Window_Exit.SetActive(true);
    }
}
