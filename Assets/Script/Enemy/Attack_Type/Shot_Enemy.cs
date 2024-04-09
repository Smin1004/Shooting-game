using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot_Enemy : Enemy_Base
{
    public GameObject bullet;
    public Transform me;
    public GameObject charge;

    public float bullet_speed;
    public float max_bullet_delay;
    public Vector2 direction;
    float cur_bullet_delay;

    protected override void DieDestroy()
    {
        GameManager.Instance.Score += 400;
        Destroy(gameObject);
    }

    private void Update()
    {
        direction = Player.Instance.playerpos - transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, direction);
        me.eulerAngles = new Vector3(0, 0, angle + 90);

        cur_bullet_delay += Time.deltaTime;

        if(cur_bullet_delay >= max_bullet_delay - 1f){
            charge.SetActive(true);
        }

        if (cur_bullet_delay >= max_bullet_delay)
        {
            StartCoroutine(fire());
            cur_bullet_delay = 0;
            charge.SetActive(false);
        }
    }

    IEnumerator fire()
    {
        var dir = Player.Instance.transform.position - transform.position;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < 5; i++)
        {
            if (i == 0) fire(10);

            var temp = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, z - 90f)).GetComponent<Enemy_Bullet>();
            temp.moveSpeed = bullet_speed;

            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }

    void fire(int count)
    {
        for (int i = 0; i < 360; i += 360 / count)
        {
            var temp = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i)).GetComponent<Enemy_Bullet>();
            temp.moveSpeed = bullet_speed - 4f;
        }
    }
}
