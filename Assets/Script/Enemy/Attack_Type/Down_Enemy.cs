using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Down_Enemy : Enemy_Base
{
    
    public float moveSpeed;

    protected override void DieDestroy(){
        GameManager.Instance.Score += 100;
        Destroy(gameObject);
    }

    private void Update() {
        transform.Translate(Vector3.down * Time.deltaTime * moveSpeed);

        if(transform.position.y <= -7f) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if(collider.CompareTag("Player")){
            collider.GetComponent<Player>()?.Damage((int)HP);
            Destroy(gameObject);
        }
    }
}
