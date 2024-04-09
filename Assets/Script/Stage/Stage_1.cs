using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_1 : Stage_Base
{
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

    IEnumerator Wave1()
    {

        for (int i = 0; i < 5; i++)
        {
            var temp = Instantiate(enemies[0], new Vector3(-3, 6), Quaternion.identity);

            if (i == 4) temp.dieAction += () => SpawnItem(0, temp.transform);

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(3);
    }

    IEnumerator Wave2()
    {

        Instantiate(enemies[1], new Vector3(-1.5f, 4), Quaternion.identity);
        Instantiate(enemies[1], new Vector3(1.5f, 4), Quaternion.identity);

        for (int i = 0; i < 5; i++)
        {
            var temp = Instantiate(enemies[0], new Vector3(3, 6), Quaternion.identity);
            temp.HP += 8;
            if (i == 1) temp.dieAction += () => SpawnItem(3, temp.transform);

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(5);
    }

    IEnumerator Wave3()
    {
        for (int i = 0; i < 4; i++)
        {
            int ranpos = Random.Range(-3, 3);

            var temp = Instantiate(enemies[4], new Vector3(ranpos, 6), Quaternion.identity);

            if (i == 2) temp.dieAction += () => SpawnItem(2, temp.transform);
            if (i == 3) temp.dieAction += () => SpawnItem(0, temp.transform);

            yield return new WaitForSeconds(2f);
        }

        yield return new WaitForSeconds(3);
    }

    IEnumerator Wave4()
    {
        var temp = Instantiate(enemies[2], new Vector3(2, -6), Quaternion.identity);
        temp.dieAction += () => SpawnItem(1, temp.transform);
        yield return new WaitForSeconds(2.8f);

        var temp1 = Instantiate(enemies[2], new Vector3(-2, -6), Quaternion.identity);
        temp1.dieAction += () => SpawnItem(0, temp1.transform);

        yield return new WaitForSeconds(7f);
    }

    IEnumerator Wave5()
    {
        List<Enemy_Base> curEnemy = new List<Enemy_Base>();

        for (int i = 0; i < 4; i++)
        {
            var temp = Instantiate(enemies[3]);
            temp.GetComponent<Bomb_Enemy>().InitChopper(new Vector3(-3, 4 - (i * 2)));
            if (i == 2) temp.dieAction += () => SpawnItem(1, temp.transform);
            if (i == 3) temp.dieAction += () => SpawnItem(3, temp.transform);
            curEnemy.Add(temp);

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 4; i++)
        {
            if (curEnemy[i] == null) continue;

            curEnemy[i].GetComponent<Bomb_Enemy>().isBoom = true;

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(3f);
    }

    IEnumerator Wave6()
    {
        List<Enemy_Base> curEnemy = new List<Enemy_Base>();

        for (int i = 0; i < 4; i++)
        {
            var temp = Instantiate(enemies[3]);
            temp.GetComponent<Bomb_Enemy>().InitChopper(new Vector3(3, 4 - (i * 2)));
            if (i == 2) temp.dieAction += () => SpawnItem(0, temp.transform);
            if (i == 3) temp.dieAction += () => SpawnItem(1, temp.transform);
            curEnemy.Add(temp);

            yield return new WaitForSeconds(0.1f);
        }

        for (int i = 0; i < 4; i++)
        {
            if (curEnemy[i] == null) continue;

            curEnemy[i].GetComponent<Bomb_Enemy>().isBoom = true;

            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(4f);
    }

    IEnumerator Boss_Wave()
    {
        yield return new WaitForSeconds(1);

        Instantiate(enemies[5], boss_pos.transform);
    }

    void SpawnItem(int index, Transform transform)
    {
        Instantiate(items[index], transform.position, Quaternion.identity);
    }
}
