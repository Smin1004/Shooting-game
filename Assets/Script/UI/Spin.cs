using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed;
    public float dir;
    bool isspin;

    void Update()
    {
        StartCoroutine(spin());
        dir += Time.deltaTime * speed;
    }

    IEnumerator spin(){
        if(isspin) yield break;
        isspin = true;

        transform.eulerAngles = new Vector3(0,0,dir);
        yield return new WaitForSeconds(0.001f);
        isspin = false;
    }
}
