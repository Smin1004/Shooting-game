using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage_UI : MonoBehaviour
{
    Image image;

    public float nowcolor;
    bool isdown;

    private void Start() {
        image = GetComponent<Image>();
    }

    void Update()
    {
        StartCoroutine(alpha(nowcolor));
        nowcolor -= Time.deltaTime;
    }

    IEnumerator alpha(float a)
    {
        if (isdown) yield break;
        isdown = true;

        image.color = new Color(255,255,255, a);
        yield return new WaitForSeconds(0.001f);
        isdown = false;
    }
}
