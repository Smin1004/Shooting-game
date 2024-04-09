using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance = null;
    public static GameManager Instance => _instance;

    public ParticleSystem background;
    
    public float Score;
    public float scoreIncreasing;

    [Header("UI")]
    public MainCanvas canvas;

    [Header("Stage Info")]
    public int stageIndex;
    public List<Stage_Base> stages = new List<Stage_Base>();
    Stage_Base curStage;

    [Header("Player")]
    public Transform p_spawn_point;
    public Player player;

    private void Awake() {
        _instance = this;
    }

    private void Start() {

        Score = TempData.Instance.stageScore;
        stageIndex = TempData.Instance.stageIndex;
        
        var bg = background.trails;

        if(stageIndex == 0) bg.lifetime = 0.005f;
        else if(stageIndex == 1) bg.lifetime = 0.01f;
        else if(stageIndex == 2) bg.lifetime = 0.02f;

        stages = GetComponentsInChildren<Stage_Base>().ToList();
        curStage = stages[stageIndex];

        StartCoroutine(IntroLogic());
    }

    private void Update() {
        ScoreSetting();
        Cheatkey();
    }

    IEnumerator IntroLogic()
    {
        Instantiate(player, p_spawn_point.position, Quaternion.identity);

        yield return new WaitForSeconds(1);
        yield return StartCoroutine(Player.Instance.Intro());

        yield return StartCoroutine(curStage.StageRoutine());

        yield break;
    }

    void Cheatkey(){
        if(Input.GetKeyDown(KeyCode.F1)) Player.Instance.Bomb();
        if(Input.GetKeyDown(KeyCode.F2)) Player.Instance.weaponLevel = 3;
        if(Input.GetKeyDown(KeyCode.F3)) Player.Instance.SkillMax();
        if(Input.GetKeyDown(KeyCode.F4)) Player.Instance.HP = 100;
        if(Input.GetKeyDown(KeyCode.F5)) Player.Instance.curGas = 100;
        if(Input.GetKeyDown(KeyCode.F6)){
            TempData.Instance.stageScore = Score;

            if(stageIndex < 2) {
                TempData.Instance.stageIndex++; 
                SceneManager.LoadScene("InGame");
           }else{
                SceneManager.LoadScene("Ranking");
            }
        }
    }

    public void DieLogic(){
        TempData.Instance.stageScore = Score;
        SceneManager.LoadScene("Ranking");
    }

    public IEnumerator EndLogic()
    {
        yield return StartCoroutine(Player.Instance.Outro());

        // 게임 클리어 시

        //score 저장, 위치 수정해야함
        TempData.Instance.stageScore = Score;

        // 조건문 0에서 수정할 것 0은 테스트 용임
        if(stageIndex < 2) {
            TempData.Instance.stageIndex++; 
            SceneManager.LoadScene("InGame");
        }else{
            SceneManager.LoadScene("Ranking");
        }
        yield break;
    }

    void ScoreSetting()
    {
        canvas.scoreBox.SetScore(Score);

        Score += scoreIncreasing * Time.deltaTime; // �⺻ ���ھ� ��
    }
}
