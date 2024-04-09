using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gas : MonoBehaviour
{
    public void SetGas(float gas, float maxgas)
    {
        float rot = Mathf.Lerp(120, -120, gas/ maxgas);//Easing.easeOutSine(gas / maxgas)

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rot));
    }
}
