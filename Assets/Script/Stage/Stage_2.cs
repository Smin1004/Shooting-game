using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_2 : Stage_Base
{
    public GameObject meteor;

    private void Start()
    {
        waveList.Add(Wave0);
        waveList.Add(Wave1);
        waveList.Add(Wave2);
        waveList.Add(Wave3);
        waveList.Add(Wave4);
        waveList.Add(Wave5);
        waveList.Add(Wave6);
        waveList.Add(Boss_Wave);
        
    }

    IEnumerator Wave0(){
        text.text = string.Format("Stage " + TempData.Instance.stageIndex);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(3f);
        text.gameObject.SetActive(false);
    }

    IEnumerator Wave1(){
        for (int i = 0; i < 4; i++)
        {
            int ranpos = Random.Range(-3, 3);

            var temp = Instantiate(enemies[0], new Vector3(ranpos, 6), Quaternion.identity);

            if (i == 1) temp.dieAction += () => SpawnItem(0, temp.transform);
            if (i == 3) temp.dieAction += () => SpawnItem(2, temp.transform);

            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(3);
    }

    IEnumerator Wave2(){
        var temp = Instantiate(enemies[1], new Vector3(2, -6), Quaternion.identity);
        temp.dieAction += () => SpawnItem(1, temp.transform);
        yield return new WaitForSeconds(2.3f);

        var temp1 = Instantiate(enemies[1], new Vector3(-2, -6), Quaternion.identity);
        temp1.dieAction += () => SpawnItem(0, temp1.transform);
        yield return new WaitForSeconds(7);
    }

    IEnumerator Wave3(){
        for (int i = 0; i < 5; i++)
        {
            int ranpos = Random.Range(-3, 3);

            var temp1 = Instantiate(enemies[2], new Vector3(ranpos, 6), Quaternion.identity);
            temp1.HP += 50;
            if(i == 2) temp1.dieAction += () => SpawnItem(2, temp1.transform);
            if(i == 4) temp1.dieAction += () => SpawnItem(0, temp1.transform);

            if(i % 2 == 0){
                var temp = Instantiate(enemies[3]).GetComponent<Bomb_Enemy>();
                temp.InitChopper(new Vector3(3, 4));
                temp.Timer = 2;
                temp.isBoom = true;
            }else{
                var temp = Instantiate(enemies[3]).GetComponent<Bomb_Enemy>();
                temp.InitChopper(new Vector3(-3, 4));
                temp.Timer = 2;
                temp.isBoom = true;
            }

            yield return new WaitForSeconds(3f);
        }

        yield return new WaitForSeconds(3f);
    }

    IEnumerator Wave4(){
        for(int i = 0; i < 7; i++){

            var temp = Instantiate(enemies[2], new Vector3(-3 + i, 4), Quaternion.identity);
            temp.HP += 60;

            if(i == 3) temp.dieAction += () => SpawnItem(0, temp.transform);
            if(i == 6) temp.dieAction += () => SpawnItem(1, temp.transform);
        }

        yield return new WaitForSeconds(7f);
    }

    IEnumerator Wave5(){

        Instantiate(enemies[4], new Vector3(-1.5f, 4), Quaternion.identity);
        Instantiate(enemies[4], new Vector3(1.5f, 4), Quaternion.identity);

        int _pos = 0;
        for (int i = 0; i < 5; i++)
        {
            int ranpos = Random.Range(-3, 3);
            while(_pos == ranpos) ranpos = Random.Range(-3, 3);

            var temp = Instantiate(meteor, new Vector3(ranpos, 0), Quaternion.identity).GetComponent<Meteor_Warning>();
            temp.MoveSpeed(5);

            _pos = ranpos;

            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(3);
    }

    IEnumerator Wave6(){
        for(int i = 0; i < 7; i++){

            var temp = Instantiate(meteor, new Vector3(-3 + i, 0), Quaternion.identity).GetComponent<Meteor_Warning>();
            temp.HPUP(1000);
            temp.MoveSpeed(2);
        }

        yield return new WaitForSeconds(8f);
    }

    IEnumerator Boss_Wave(){
        yield return new WaitForSeconds(1);

        Instantiate(enemies[5], boss_pos.transform);
    }

    void SpawnItem(int index, Transform transform)
    {
        Instantiate(items[index], transform.position, Quaternion.identity);
    }
}
