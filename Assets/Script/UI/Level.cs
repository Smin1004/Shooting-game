using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public List<GameObject> Lv = new List<GameObject>();

    public void Cur_Lv(int level){
        for(int i = 0; i < level + 1; i++){
            Lv[i].SetActive(true);
        }
    }
}
