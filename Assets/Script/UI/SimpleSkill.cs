using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSkill : MonoBehaviour
{
    public Image fillGauge;
    public Image Wait;

    bool isWait;

    public void SetFill(float fillAmount)
    {
        fillGauge.fillAmount = fillAmount;

        fillGauge.gameObject.SetActive(fillAmount > 0f);
    }

    public IEnumerator Wait_Time()
    {
        if(isWait) yield break;
        isWait = true;

        Wait.gameObject.SetActive(true);
        yield return new WaitForSeconds(1);
        Wait.gameObject.SetActive(false);

        isWait = false;
    }
}