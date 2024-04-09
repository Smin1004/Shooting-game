using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P_UI : MonoBehaviour
{
    public GameObject die_boom;
    public Image damage_ui;
    public Camera main_camera;

    bool isCamMove;

    public Vector3 original_campos;

    private void Start()
    {
        damage_ui = GameManager.Instance.canvas.damage_ui;
        main_camera = GameManager.Instance.canvas.InGameCamera;
        original_campos = main_camera.transform.position;
    }

    public void damage()
    {
        damage_ui.color = new Color(255, 255, 255, 1);
        damage_ui.GetComponent<Damage_UI>().nowcolor = 1;
        StartCoroutine(CameraMove());
    }

    IEnumerator CameraMove()
    {
        if (isCamMove) yield break;

        isCamMove = true;

        float ranx = Random.Range(-1, 2);
        if (ranx == 0) ranx = -1;
        float rany = Random.Range(-1, 2);
        if (rany == 0) rany = 1;

        main_camera.transform.position = main_camera.transform.position + new Vector3(ranx / 10, rany / 10);
        yield return new WaitForSeconds(0.2f);
        main_camera.transform.position = original_campos;

        isCamMove = false;
    }

    public IEnumerator Die_Boom(float plus)
    {
        float ranx = 0;
        float rany = 0;
        while (true)
        {
            ranx = Random.Range(-plus, plus + 1);
            rany = Random.Range(-plus / 2, (plus + 1) / 2);
            Instantiate(die_boom, transform.position + new Vector3(ranx, rany), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
