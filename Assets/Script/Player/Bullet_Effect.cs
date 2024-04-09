using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Effect : MonoBehaviour
{
    private void Start() {
        Invoke("Destory_this", 0.35f);
    }

    void Destory_this(){
        Destroy(gameObject);
    }
}
