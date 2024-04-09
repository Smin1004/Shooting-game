using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Enemy : Enemy_Base
{
    public GameObject bullet;
    public float Timer;
    public float bullet_speed;
    public int bullet_count;
    public bool isBoom;

    Vector3 finalPos;

    protected override void DieDestroy()
    {
        GameManager.Instance.Score += 100;
        Destroy(gameObject);
    }

    private void Update()
    {
        // 돚거해옴, 물어봐야함
        if (Vector3.Distance(transform.position, finalPos) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, finalPos, Time.deltaTime * 15f);
            return;
        }

        if(isBoom){
            StartCoroutine(Bomb(Timer, bullet_count));
            isBoom = false;
        }
    }

    // 돚거해옴, 물어봐야함
    public void InitChopper(Vector3 _finalPos)
    {
        finalPos = _finalPos;

        transform.position = finalPos + new Vector3(2 * RandomBetween(1, -1), 6);
    }

    IEnumerator Bomb(float timer, int count)
    {
        yield return new WaitForSeconds(timer);

        for (int i = 0; i < 360; i += 360 / count)
        {
            var temp = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i)).GetComponent<Enemy_Bullet>();
            temp.moveSpeed = bullet_speed;
        }

        DieDestroy();
    }

    // 돚거해옴, 물어봐야함
    T RandomBetween<T>(params T[] objs)
    {
        return objs[Random.Range(0, objs.Length)];
    }
}
