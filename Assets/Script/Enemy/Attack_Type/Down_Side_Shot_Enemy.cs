using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Side_Shot_Enemy : Enemy_Base
{
    public GameObject bullet;

    public float moveSpeed;
    public float bullet_speed;
    public float max_bullet_delay;
    float cur_bullet_delay;

    protected override void DieDestroy()
    {
        GameManager.Instance.Score += 300;
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        cur_bullet_delay += Time.deltaTime;

        if (transform.position.y <= -7f) Destroy(gameObject);

        if(cur_bullet_delay >= max_bullet_delay){
            fire();
            cur_bullet_delay = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Player>()?.Damage((int)HP);
            Destroy(gameObject);
        }
    }

    void fire()
    {
        var temp = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, 90)).GetComponent<Enemy_Bullet>();
        temp.moveSpeed = bullet_speed;
        var temp1 = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, -90)).GetComponent<Enemy_Bullet>();
        temp1.moveSpeed = bullet_speed;
    }
}
