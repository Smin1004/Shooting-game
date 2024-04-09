using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion : MonoBehaviour
{
    public GameObject bullet;
    Transform target;
    Vector3 offset;

    public void InitDrone(Transform _target, Vector3 _offset)
    {
        target = _target;
        offset = _offset;
    }

    private void Update()
    {
        if (target == null) return;

        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * 10f);
    }

    public void DroneAttack()
    {
        Instantiate(bullet, transform.position, Quaternion.identity);
    }

}