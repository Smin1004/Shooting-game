using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : Enemy_Base
{
    public Transform me;
    public float speed;
    public float dir;
    public float moveSpeed;
    bool isspin;

    protected override void DieDestroy()
    {
        GameManager.Instance.Score += 2000;
        Destroy(gameObject);
    }

    private void Update() {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);
        StartCoroutine(spin());
        dir += Time.deltaTime * speed;
        if(transform.position.y <= -14f) Destroy(gameObject);
    }

    IEnumerator spin(){
        if(isspin) yield break;
        isspin = true;

        me.eulerAngles = new Vector3(0,0,dir);
        yield return new WaitForSeconds(0.001f);
        isspin = false;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            collider.GetComponent<Player>()?.Damage(50);
            Destroy(gameObject);
        }
    }
}
