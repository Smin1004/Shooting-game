using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    public P_UI pui;

    public Camera InGameCamera;
    public Image damage_ui;

    public Hp_Box playerHP;
    public Gas playerGas;

    public Level level;
    public ScoreBox scoreBox;
    public SimpleSkill Repair;
    public SimpleSkill Bomb;

    public SimpleGauge bossHP;
}
