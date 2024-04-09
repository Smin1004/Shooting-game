using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Easing : MonoBehaviour
{
    public static float easeInQuint(float x) => x * x * x * x * x;
    public static float easeOutQuint(float x) => 1 - Mathf.Pow(1 - x, 5);
    public static float easeOutSine(float x) => Mathf.Sin((x * Mathf.PI) / 2);
    public static float easeInOutQuart(float x) => x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2;
}
