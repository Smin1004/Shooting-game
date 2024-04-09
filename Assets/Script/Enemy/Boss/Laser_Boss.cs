using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Boss : Enemy_Base
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
    public float laser_time;
    bool isfire;

    [Header("Laser")]
    public int laser_count;
    public float laser_daley;

    [Header("All_Laser")]
    public Enemy_Base enemy;
    public Item_Base item;
    public float all_laser_delay;

    [Header("Side_Laser")]
    public float side_laser_shot_time;
    public int side_laser_bullet_count;
    public float side_laser_bullet_speed;
    public float side_laser_bullet_delay;

    [Header("X_Laser")]
    public List<Transform> x_point = new List<Transform>();
    public float x_laser_shot_time;
    public float x_laser_delay;

    protected override void DieDestroy()
    {
        isintro = true;
        Player.Instance.isfire = true;
        GameManager.Instance.Score += 5000;
        StartCoroutine(Boom(1));
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
        if (isintro) return;

        if (!isfire) cur_bullet_delay += Time.deltaTime;

        if (cur_bullet_delay >= max_bullet_delay && isfire == false)
        {
            isfire = true;
            int pattern = Random.Range(0, 4);
            StartCoroutine(shot_pattern(pattern));
        }
    }

    IEnumerator Boom(float plus)
    {
        float ranx = 0;
        float rany = 0;
        while (true)
        {
            ranx = Random.Range(-plus, plus + 1);
            rany = Random.Range(-plus / 2, (plus + 1) / 2);
            Instantiate(boom, transform.position + new Vector3(ranx, rany), Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Outro()
    {
        yield return new WaitForSeconds(5f);

        yield return StartCoroutine(MoveTo(new Vector3(0, 7), 1, 0));

        StartCoroutine(GameManager.Instance.EndLogic());
    }

    IEnumerator Intro()
    {
        yield return StartCoroutine(MoveTo(new Vector3(0, 10), 2.5f, 0));
        yield return new WaitForSeconds(1);
        yield return StartCoroutine(MoveTo(new Vector3(0, 2.8f), 2f, 1));
        bossHp.gameObject.SetActive(true);
        isintro = false;
        Player.Instance.isfire = false;
    }

    IEnumerator Warning()
    {
        yield return new WaitForSeconds(1f);

        Instantiate(warning_up, new Vector3(-14.5f, 4.25f), Quaternion.identity);
        Instantiate(warning_down, new Vector3(14.5f, -4.25f), Quaternion.identity);

        StartCoroutine(Intro());
    }

    IEnumerator shot_pattern(int pattern)
    {
        switch (pattern)
        {
            case 0:
                yield return Laser(laser_count, laser_daley);
                isfire = false;
                break;

            case 1:
                yield return All_Laser(all_laser_delay);
                isfire = false;
                break;
            
            case 2:
                yield return Side_Laser(side_laser_shot_time, side_laser_bullet_count, side_laser_bullet_speed, side_laser_bullet_delay);
                isfire = false;
                break;
            
            case 3:
                yield return X_Laser(x_laser_shot_time, x_laser_delay);
                isfire = false;
                break;
        }
    }

    IEnumerator Laser(int count, float delay)
    {
        int ran = Random.Range(0, 2);

        for (int k = 0; k < count; k++)
        {
            if (isintro) yield break;

            if ((k + ran) % 2 == 0)
            {
                for (int i = 0; i < Point.Count - 1; i++)
                {
                    Instantiate(laser, new Vector3(Point[i].transform.position.x, Point[i].transform.position.y - 1), Point[i].rotation);
                    yield return new WaitForSeconds(delay);
                }
            }
            else
            {
                for (int i = Point.Count - 1; i > 0; i--)
                {
                    Instantiate(laser, new Vector3(Point[i].transform.position.x, Point[i].transform.position.y - 1), Point[i].rotation);
                    yield return new WaitForSeconds(delay);
                }
            }

            yield return new WaitForSeconds(laser_time);
        }

        yield return new WaitForSeconds(1);

        cur_bullet_delay = 0;
    }

    IEnumerator All_Laser(float delay)
    {
        int point = Random.Range(0, Point.Count);

        var temp = Instantiate(enemy, Point[point]);
        temp.HP += 50;
        temp.GetComponent<Enemy_Base>().dieAction += () => SpawnItem(temp.transform);

        yield return new WaitForSeconds(delay);

        for (int i = 0; i < Point.Count; i++)
        {
            Instantiate(laser, new Vector3(Point[i].transform.position.x, Point[i].transform.position.y - 1), Point[i].rotation);
        }

        yield return new WaitForSeconds(laser_time);

        cur_bullet_delay = 0;
    }

    IEnumerator Side_Laser(float shot_time,int count, float bullet_speed, float delay)
    {
        float interval = 0;

        var L1_temp = Instantiate(laser, new Vector3(Point[0].transform.position.x, Point[0].transform.position.y - 1), Point[0].rotation).GetComponent<Laser_Charge>();
        var L2_temp = Instantiate(laser, new Vector3(Point[3].transform.position.x, Point[3].transform.position.y - 1), Point[3].rotation).GetComponent<Laser_Charge>();

        L1_temp.shot_time = shot_time + 0.5f;
        L2_temp.shot_time = shot_time + 0.5f;

        yield return new WaitForSeconds(1.5f);

        for (int i = 0; i < count; i++)
        {
            int point = Random.Range(0, 2) + 1;
            interval = -0.5f;
            for (int j = 0; j < 3; j++)
            {
                var temp = Instantiate(bullet, new Vector3(Point[point].transform.position.x + interval, Point[point].transform.position.y - 1), 
                                       Quaternion.Euler(0,0,180)).GetComponent<Enemy_Bullet>();
                temp.moveSpeed = bullet_speed;
                interval += 0.5f;
            }
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(1);

        cur_bullet_delay = 0;
    }

    IEnumerator X_Laser(float shot_time, float delay){
        var L1_temp = Instantiate(laser, x_point[0].transform.position, x_point[0].rotation).GetComponent<Laser_Charge>();
        L1_temp.shot_time = shot_time + delay * 2;

        yield return new WaitForSeconds(delay);

        var L2_temp = Instantiate(laser, x_point[1].transform.position, x_point[1].rotation).GetComponent<Laser_Charge>();
        L2_temp.shot_time = shot_time + delay;

        yield return new WaitForSeconds(delay);

         var L3_temp = Instantiate(laser, x_point[2].transform.position, x_point[2].rotation).GetComponent<Laser_Charge>();
        L3_temp.shot_time = shot_time;

        yield return new WaitForSeconds(shot_time + 1);

        cur_bullet_delay = 0;
    }

    IEnumerator MoveTo(Vector3 target, float sec, int lerp)
    {
        float timer = 0f;
        Vector3 start = transform.position;

        while (timer <= sec)
        {
            switch (lerp)
            {
                case 0:
                    transform.position = Vector3.Lerp(start, target, Easing.easeInQuint(timer / sec));
                    break;

                case 1:
                    transform.position = Vector3.Lerp(start, target, Easing.easeOutQuint(timer / sec));
                    break;
            }
            timer += Time.deltaTime;
            yield return null;
        }

        yield break;
    }

    void SpawnItem(Transform transform)
    {
        Instantiate(item, transform.position, Quaternion.identity);
    }

}
