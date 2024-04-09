using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleGauge : MonoBehaviour
{
    public Image gauge;

    public void SetGauge(float fillAmount)
    {
        gauge.fillAmount = fillAmount;
    }
}
