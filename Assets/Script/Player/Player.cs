using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player _instance = null;
    public static Player Instance => _instance;

    [Header("Objects")]
    public GameObject bullet;
    public Minion minion;
    List<Minion> mini = new List<Minion>();
    public GameObject bomb;
    P_UI pui;


    [Header("anim")]
    public GameObject Idle;
    public GameObject Left;
    public GameObject Right;
    public GameObject shield;

    [Header("Stats")]
    public int HP;
    int maxHP;
    public float curGas;
    float maxGas;
    public float gasSpeed;
    public int weaponLevel;
    public float moveSpeed;
    float cur_bullet_delay;
    public float max_bullet_delay;
    float cur_Shield_delay;
    public float max_Shield_delay;
    float x, y;

    bool isMoveOn;
    bool isShield;
    bool isDamage;
    bool isMinion;
    public bool isfire;
    public bool isLaser;

    [Header("Skill")]
    float cur_repair_time;
    public float max_repair_time;
    float cur_bomb_time;
    public float max_bomb_time;

    [Header("Pos")]
    public Vector3 localPosition;
    public Vector3 playerpos;

    [Header("Operator")]
    public bool playerActive = false;

    void Start()
    {
        _instance = this;
        maxHP = HP;
        maxGas = curGas;
        pui = GameManager.Instance.canvas.pui;
        weaponLevel = TempData.Instance.weaponLevel;
        GameManager.Instance.canvas.level.Cur_Lv(weaponLevel);
    }

    void Update()
    {
        if (!playerActive) return;

        SetUI();
        fire();
        Move();
        SetSkill();
        curGas -= Time.deltaTime * gasSpeed;
        playerpos = transform.position;

        if (isShield)
        {
            cur_Shield_delay += Time.deltaTime;

            if (cur_Shield_delay >= max_Shield_delay)
            {
                shield.SetActive(false);
                isShield = false;
                cur_Shield_delay = 0;
            }
        }

        if (isLaser)
        {
            StartCoroutine(Laser_Damage());
        }

        if (HP <= 0 || curGas <= 0)
        {
            playerActive = false;
            StartCoroutine(pui.Die_Boom(1));
            StartCoroutine(Die());
        }
    }

    void SetUI()
    {
        GameManager.Instance.canvas.playerHP.SetHp((float)HP, (float)maxHP);
        GameManager.Instance.canvas.playerGas.SetGas(curGas, maxGas);
    }

    void Move()
    {
        if (!isMoveOn) return;

        //움직임 범위 제한
        localPosition = new Vector3(Mathf.Clamp(transform.position.x, -3.5f, 3.5f),
            Mathf.Clamp(transform.position.y, -4.5f, 4.5f), 0f);
        transform.localPosition = localPosition;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        Vector3 nor = new Vector3(x, y, 0f).normalized;
        transform.Translate(nor * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            Right.SetActive(false);
            Idle.SetActive(true);
            Left.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Right.SetActive(true);
            Idle.SetActive(false);
            Left.SetActive(false);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Right.SetActive(false);
            Idle.SetActive(false);
            Left.SetActive(true);
        }
        else
        {
            Right.SetActive(false);
            Idle.SetActive(true);
            Left.SetActive(false);
        }
    }

    void fire()
    {
        cur_bullet_delay += Time.deltaTime;

        if (Input.GetKey(KeyCode.Z) && cur_bullet_delay >= max_bullet_delay && !isfire)
        {
            Bullet_fire(); // 총알 레벨업 시 다르게 나가게 할것

            //딜레이 초기화
            cur_bullet_delay = 0;
        }
    }

    public void WeaponLevelUp()
    {
        if (weaponLevel == 3)
        {
            GameManager.Instance.Score += 1000;
            return;
        }

        weaponLevel++;
        GameManager.Instance.canvas.level.Cur_Lv(weaponLevel);
        TempData.Instance.weaponLevel = weaponLevel;
    }

    void Bullet_fire()
    {
        if(weaponLevel == 0) Instantiate(bullet, transform.position, transform.rotation);
        else {
            Instantiate(bullet, new Vector3(transform.position.x - 0.2f, transform.position.y), transform.rotation);
            Instantiate(bullet, new Vector3(transform.position.x + 0.2f, transform.position.y), transform.rotation);

            if(weaponLevel >= 2){
                Instantiate(bullet, new Vector3(transform.position.x - 0.5f, transform.position.y - 0.3f), transform.rotation);
                Instantiate(bullet, new Vector3(transform.position.x + 0.5f, transform.position.y - 0.3f), transform.rotation);

                if(weaponLevel == 3){

                    if(!isMinion){
                        isMinion = true;
                        Spawn_minion(new Vector3(1f, -0.5f));
                        Spawn_minion(new Vector3(-1f, -0.5f));
                    }

                    mini[0].DroneAttack();
                    mini[1].DroneAttack();
                }
            }
        }
    }

    void Spawn_minion(Vector3 pos)
    {
        var drone = Instantiate(minion);
        drone.InitDrone(transform, pos);
        mini.Add(drone);
    }

    public IEnumerator Intro()
    {
        playerActive = false;
        yield return StartCoroutine(MoveTo(new Vector3(0, -2), 2, 1));
        playerActive = true;
        isMoveOn = true;
    }

    public IEnumerator Outro()
    {
        isMoveOn = false;
        playerActive = false;
        Right.SetActive(false);
        Idle.SetActive(true);
        Left.SetActive(false);
        yield return StartCoroutine(MoveTo(new Vector3(0, -2), 2, 0));
        yield return StartCoroutine(MoveTo(new Vector3(0, 8), 3, 0));
    }

    IEnumerator Die()
    {
        yield return StartCoroutine(MoveTo(new Vector3(transform.position.x, transform.position.y - 2), 4, 1));
        GameManager.Instance.DieLogic();
    }

    IEnumerator Laser_Damage()
    {
        if (isShield) yield break;

        if (isDamage) yield break;

        isDamage = true;

        pui.damage();

        HP -= 1;
        yield return new WaitForSeconds(0.05f);

        isDamage = false;
    }

    public void Damage(int Damage)
    {
        if (isShield) return;

        StartCoroutine(damage(Damage));

        IEnumerator damage(int Damage)
        {

            if (isDamage) yield break;

            isDamage = true;

            pui.damage();

            HP -= Damage;
            yield return null;
            isDamage = false;
        }
    }

    void SetSkill()
    {
        cur_repair_time += Time.deltaTime;
        cur_bomb_time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X) && cur_repair_time >= max_repair_time)
        {
            Repair(30f);
            cur_repair_time = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.X) && cur_repair_time < max_repair_time)
            StartCoroutine(GameManager.Instance.canvas.Repair.Wait_Time());

        if (Input.GetKeyDown(KeyCode.C) && cur_bomb_time >= max_bomb_time)
        {
            Bomb();
            cur_bomb_time = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.C) && cur_bomb_time < max_bomb_time)
            StartCoroutine(GameManager.Instance.canvas.Bomb.Wait_Time());


        GameManager.Instance.canvas.Repair.SetFill(1 - cur_repair_time / max_repair_time);
        GameManager.Instance.canvas.Bomb.SetFill(1 - cur_bomb_time / max_bomb_time);
    }

    public void SkillMax()
    {
        cur_bomb_time = max_bomb_time;
        cur_repair_time = max_repair_time;
    }

    public void Repair(float heal = 10f)
    {
        HP += (int)heal;
        if (HP > maxHP) HP = maxHP;
    }

    public void Repair_Gas(float heal = 10f)
    {
        curGas += (int)heal;
        if (curGas > maxGas) curGas = maxGas;
    }

    public void Bomb()
    {
        Instantiate(bomb, transform.position, transform.rotation);
    }

    public void Shield_On()
    {
        isShield = true;
        shield.SetActive(true);
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
}
