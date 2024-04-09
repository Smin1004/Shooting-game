using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Bullet : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float damage = 5f;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Wall"))
        {
            OnDestroy();
        }

        if (collider.CompareTag("Player"))
        {
            collider.GetComponent<Player>().Damage((int)damage);
            OnDestroy();
        }
    }

    public void OnDestroy() {
        Destroy(gameObject);
    }
}
