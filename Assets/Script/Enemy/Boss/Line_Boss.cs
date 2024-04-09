using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Line_Boss : Enemy_Base
{
    [Header("Obj")]
    public GameObject boom;
    public GameObject bullet;
    public GameObject laser;
    public GameObject warning_up;
    public GameObject warning_down;
    public List<Transform> Point = new List<Transform>();
    SimpleGauge bossHp;

    [Header("Stats")]
    public float max_bullet_delay;
    public float cur_bullet_delay;
    public bool isintro;
    bool isfire;

    [Header("Box_Shot")]
    public int box_shot_count;
    public int box_size;
    public float box_bullet_speed;

    [Header("Shot_Gun")]
    public int shot_gun_count;
    public int shot_gun_bullet_count;
    public float shot_gun_central;
    public float shot_gun_bullet_speed;

    [Header("Laser")]
    public int laser_count;
    public float laser_time;

    protected override void DieDestroy()
    {
        isintro = true;
        Player.Instance.isfire = true;
        GameManager.Instance.Score += 5000;
        StartCoroutine(Boom(2));
        StartCoroutine(Outro());
    }

    protected override void Start()
    {
        base.Start();
        Player.Instance.isfire = true;
        bossHp = GameManager.Instance.canvas.bossHP;

        isintro = true;
        StartCoroutine(Warning());
    }

    private void Update()
    {
        bossHp.SetGauge(HP / maxHP);
        if(isintro) return;

        if(!isfire) cur_bullet_delay += Time.deltaTime;

        if (cur_bullet_delay >= max_bullet_delay && isfire == false)
        {
            isfire = true;
            int pattern = Random.Range(0, 3);
            StartCoroutine(shot_pattern(pattern));
        }     
    }

    IEnumerator Intro(){
        yield return StartCoroutine(MoveTo(new Vector3(0, 10), 2.5f, 0));
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(MoveTo(new Vector3(0, 2.8f), 2f, 1));
        bossHp.gameObject.SetActive(true);
        isintro = false;
        Player.Instance.isfire = false;
    }

    IEnumerator Outro(){
        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(MoveTo(new Vector3(0, 7), 1, 0));

        StartCoroutine(GameManager.Instance.EndLogic());
    }

    IEnumerator Boom(float plus)
    {
        float ranx = 0;
        float rany = 0;
        while (true)
        {
            ranx = Random.Range(-plus - 2, plus + 3);
            rany = Random.Range(-plus / 2, (plus + 1) / 2);
            Instantiate(boom, transform.position + new Vector3(ranx, rany), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Warning(){
        yield return new WaitForSeconds(1f);

        Instantiate(warning_up, new Vector3(-14.5f, 4.25f), Quaternion.identity);
        Instantiate(warning_down, new Vector3(14.5f, -4.25f), Quaternion.identity);

        StartCoroutine(Intro());
    }

    IEnumerator shot_pattern(int pattern){

        switch(pattern){
            case 0: yield return StartCoroutine(box_shot(box_shot_count, box_size, box_bullet_speed));
            isfire = false;
            break;

            case 1: yield return StartCoroutine(shotgun(shot_gun_count, shot_gun_bullet_count, shot_gun_central, shot_gun_bullet_speed));
            isfire = false;
            break;

            case 2: yield return StartCoroutine(Laser(laser_count));
            isfire = false;
            break;
        }
    }

    IEnumerator box_shot(int count, int size, float bullet_speed)
    {
        float interval = 0;

        for (int k = 0; k < count; k++)
        {
            int point = Random.Range(0, Point.Count);
            for (int j = 0; j < size; j++)
            {
                interval = -0.5f;
                for (int i = 0; i < 3; i++)
                {
                    if(isintro) yield break;

                    var temp = Instantiate(bullet, new Vector3(Point[point].transform.position.x + interval, Point[point].transform.position.y),
                                                Quaternion.Euler(0,0,180)).GetComponent<Enemy_Bullet>();
                    temp.moveSpeed = bullet_speed;

                    interval += 0.5f;
                }
                yield return new WaitForSeconds(0.08f);
            }
            yield return new WaitForSeconds(0.5f);
        }

        cur_bullet_delay = 0;
    }

    IEnumerator shotgun(int count, int bullet_count, float central, float bullet_speed){
        for (int k = 0; k < count; k++)
        {
            int point = Random.Range(0, Point.Count);
            float amount = central / (bullet_count - 1);
            float z = central / -2f;

            for (int i = 0; i < bullet_count; i++)
            {
                if(isintro) yield break;
                Quaternion rot = Quaternion.Euler(0, 0, z + 180);
                var temp = Instantiate(bullet, Point[point].transform.position, rot).GetComponent<Enemy_Bullet>();
                temp.moveSpeed = bullet_speed;

                z += amount;
            }
            yield return new WaitForSeconds(1);
        }

        cur_bullet_delay = 0;
    }

    IEnumerator Laser(int count){
        for (int k = 0; k < count; k++)
        {
            if(isintro) yield break;

            int point1 = Random.Range(0, Point.Count);
            int point2 = Random.Range(0, Point.Count);
            while(point1 == point2) point2 = Random.Range(0, Point.Count);

            Instantiate(laser, new Vector3(Point[point1].transform.position.x, Point[point1].transform.position.y - 1), Point[point1].rotation);
            Instantiate(laser, new Vector3(Point[point2].transform.position.x, Point[point2].transform.position.y - 1), Point[point2].rotation);

            yield return new WaitForSeconds(laser_time);
        }

        yield return new WaitForSeconds(1);

        cur_bullet_delay = 0;
    }

    IEnumerator MoveTo(Vector3 target, float sec, int lerp)
    {
        float timer = 0f;
        Vector3 start = transform.position;

        while(timer <= sec){
            switch(lerp){
                case 0 : transform.position = Vector3.Lerp(start, target, Easing.easeInQuint(timer / sec));
                break;

                case 1 : transform.position = Vector3.Lerp(start, target, Easing.easeOutQuint(timer / sec));
                break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        yield break;
    }
}
