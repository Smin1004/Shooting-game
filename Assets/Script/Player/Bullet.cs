using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float damage;
    public GameObject particle;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collider.CompareTag("Enemy"))
        {
            collider.GetComponent<Enemy_Base>().Enemy_Damage(damage);
            GameManager.Instance.Score += 10;
            Instantiate(particle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
