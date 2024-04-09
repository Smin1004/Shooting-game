using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
   ParticleSystem particle;
   CircleCollider2D c_collider;

   public float Radius;
   public float speed;

    private void Start() {
        particle = GetComponent<ParticleSystem>();
        c_collider = GetComponent<CircleCollider2D>();
    }

    private void Update() {
        Boom();
        if(Radius >= 9){
            Invoke("OnDestroy", 2);
        }
    }

    private void OnDestroy() {
        Destroy(gameObject);
    }

    void Boom(){
        var shape = particle.shape;
        
        shape.radius = Radius;
        c_collider.radius = Radius;

        Radius += Time.deltaTime * speed;
    }

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Enemy"))
        {
            collider.GetComponent<Enemy_Base>().Enemy_Damage(200);
        }

        if(collider.CompareTag("Enemy_Bullet")){
            collider.GetComponent<Enemy_Bullet>().OnDestroy();
        }
    }
}
