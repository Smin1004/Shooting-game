using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_0 : Stage_Base
{
    public GameObject tutorial;

    private void Start()
    {
        waveList.Add(Wave0);
        waveList.Add(Wave1);
        waveList.Add(Wave2);
        waveList.Add(Wave3);
        waveList.Add(Wave4);
        waveList.Add(Wave5);
        waveList.Add(Wave6);
    }

    IEnumerator Wave0(){;
        text.text = string.Format("Stage " + TempData.Instance.stageIndex);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        text.gameObject.SetActive(false);
    }

    IEnumerator Wave1(){
        tutorial.GetComponent<Text>().text = string.Format("Press the Arrow to Move");
        yield return MoveTo(new Vector3(0,0), 2, 1);
        yield return new WaitForSeconds(2);
        yield return MoveTo(new Vector3(0, -10), 2, 0);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Wave2(){
        tutorial.GetComponent<Text>().text = string.Format("Press the Z to Attack");
        yield return MoveTo(new Vector3(0,0), 2, 1);
        yield return new WaitForSeconds(2);
        yield return MoveTo(new Vector3(0, -10), 2, 0);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Wave3(){
        tutorial.GetComponent<Text>().text = string.Format("Press the X to Hill");
        yield return MoveTo(new Vector3(0,0), 2, 1);
        yield return new WaitForSeconds(2);
        yield return MoveTo(new Vector3(0, -10), 2, 0);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Wave4(){
        tutorial.GetComponent<Text>().text = string.Format("Press the C to Bomb");
        yield return MoveTo(new Vector3(0,0), 2, 1);
        yield return new WaitForSeconds(2);
        yield return MoveTo(new Vector3(0, -10), 2, 0);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Wave5(){
        tutorial.GetComponent<Text>().text = string.Format("If HP or Gas goes to zero, you die");
        yield return MoveTo(new Vector3(0,0), 2, 1);
        yield return new WaitForSeconds(2);
        yield return MoveTo(new Vector3(0, -10), 2, 0);
        yield return new WaitForSeconds(2);
    }

    IEnumerator Wave6(){
        tutorial.GetComponent<Text>().text = string.Format("Good Luck");
        yield return MoveTo(new Vector3(0,0), 2, 1);
        yield return new WaitForSeconds(2);
        yield return MoveTo(new Vector3(0, -10), 2, 0);
        yield return new WaitForSeconds(2);
        StartCoroutine(GameManager.Instance.EndLogic());
    }

    IEnumerator MoveTo(Vector3 target, float sec, int lerp)
    {
        float timer = 0f;
        Vector3 start = tutorial.transform.position;

        while (timer <= sec)
        {
            switch (lerp)
            {
                case 0:
                    tutorial.transform.position = Vector3.Lerp(start, target, Easing.easeInQuint(timer / sec));
                    break;

                case 1:
                    tutorial.transform.position = Vector3.Lerp(start, target, Easing.easeOutQuint(timer / sec));
                    break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}
