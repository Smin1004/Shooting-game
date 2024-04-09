using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBox : MonoBehaviour
{
    public Text scoreTxt;

    public void SetScore(float score)
    {
        scoreTxt.text = string.Format("{0:#,0}", score);
    }
}