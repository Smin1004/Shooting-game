using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Shot_Enemy : Enemy_Base
{
    public GameObject bullet;
    public GameObject charge;

    public float moveSpeed;
    public float bullet_speed;
    public float max_bullet_delay;
    float cur_bullet_delay;

    protected override void DieDestroy()
    {
        GameManager.Instance.Score += 100;
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        cur_bullet_delay += Time.deltaTime;

        if (transform.position.y <= -7f) Destroy(gameObject);

        if(cur_bullet_delay >= max_bullet_delay - 0.5f){
            charge.SetActive(true);
        }

        if(cur_bullet_delay >= max_bullet_delay){
            fire(8);
            cur_bullet_delay = 0;   
            charge.SetActive(false);
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

    void fire(int count)
    {
        for (int i = 0; i < 360; i += 360 / count)
        {
            var temp = Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, i)).GetComponent<Enemy_Bullet>();
            temp.moveSpeed = bullet_speed;
        }
    }
}
