using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public TempData temp;
    
    private void Start() {
        temp.InitTempData();
        SceneManager.LoadScene("Title");
    }
}
