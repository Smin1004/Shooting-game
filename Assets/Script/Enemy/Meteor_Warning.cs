using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_Warning : MonoBehaviour
{
    public GameObject meteor;
    public float wait_time;
    
    float alpha = 1;
    int updown;

    SpriteRenderer sp;

    private void Start() {
        sp = GetComponent<SpriteRenderer>();
        StartCoroutine(color());
        StartCoroutine(shot());
    }

    IEnumerator color(){
        while(true){
            if(alpha == 1) updown = -1;
            else if(alpha <= 0.4) updown = 1;

            sp.color = new Color(255, 255, 255, alpha);
            alpha += 0.1f * updown;

            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator shot(){
        yield return new WaitForSeconds(wait_time);
        Instantiate(meteor, new Vector3(transform.position.x, 7, 0), transform.rotation);
        Destroy(gameObject);
    }

    public void MoveSpeed(float speed){
        meteor.GetComponent<Meteor>().moveSpeed = speed;
    }

    public void HPUP(float Hp){
        meteor.GetComponent<Enemy_Base>().HP = Hp;
    }
}
