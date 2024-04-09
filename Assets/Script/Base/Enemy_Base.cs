using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    public float maxHP;
    public float HP;

    bool isDie;

    public Action dieAction;

    protected virtual void Start()
    {
        HP = maxHP;
        dieAction += DieDestroy;
    }

    public virtual void Enemy_Damage(float Damage)
    {
        HP -= Damage;

        if (HP <= 0)
        {
            if(isDie) return;
            isDie = true;

            dieAction?.Invoke();
        }
    }

    //총 맞고 죽었을때 효과
    protected abstract void DieDestroy();
}
