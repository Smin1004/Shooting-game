using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp_Box : MonoBehaviour
{
    public Animator hpAnim;
    public Text hpTxt;
    public TrailRenderer hpTrail;

    public void SetHp(float hp, float maxHp)
    {
        hpTxt.text = string.Format("{0:0}/{1:0}", hp, maxHp);

        Color hpColor;
        float calc = hp / maxHp;

        if (calc >= 0.8f)
        {
            hpColor = Color.green;
        }
        else if (calc >= 0.3f)
        {
            hpColor = Color.yellow;
        }
        else
        {
            hpColor = Color.red;
        }

        hpTxt.color = hpColor;
        hpTrail.endColor = hpColor;
        hpTrail.startColor = hpColor;

        if (calc < 0f)
        {
            hpAnim.SetBool("die", true);
        }
    }
}
