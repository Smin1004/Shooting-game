using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Up_Enemy : Enemy_Base
{
    public GameObject bullet;

    public float bullet_speed;
    public float moveSpeed;
    public float max_bullet_delay;
    float cur_bullet_delay;
    
    int speedDown = 1;

    protected override void DieDestroy()
    {
        GameManager.Instance.Score += 700;
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed / speedDown);

        if(transform.position.y >= 7) Destroy(gameObject);

        cur_bullet_delay += Time.deltaTime;
        if (transform.position.y >= 1.5f)
        {
            speedDown = 2;
            if (cur_bullet_delay >= max_bullet_delay)
            {
                StartCoroutine(fire());
                cur_bullet_delay = 0;
            }
        }
    }

    IEnumerator fire()
    {
        float interval = -0.8f;

        var dir = Player.Instance.transform.position - transform.position;
        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        for (int i = 0; i < 5; i++)
        {
            var temp = Instantiate(bullet, new Vector3(transform.position.x + interval, transform.position.y), Quaternion.Euler(0, 0, z - 90f)).GetComponent<Enemy_Bullet>();
            temp.moveSpeed = bullet_speed;

            interval += 0.4f;
        }

        yield break;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            collider.GetComponent<Player>().Damage(20);
        }
    }
}
