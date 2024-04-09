using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Stage_Base : MonoBehaviour
{
    public Enemy_Base[] enemies;
    public Item_Base[] items;
    public Transform boss_pos;
    public Text text;

    protected List<Func<IEnumerator>> waveList = new List<Func<IEnumerator>>();

    public IEnumerator StageRoutine()
    {
        yield return null;
        foreach (var item in waveList)
        {
            yield return StartCoroutine(item?.Invoke());
        }
        yield break;
    }
}
