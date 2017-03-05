using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour {

    public float cooldown;
    public Bullet bullet;
    public float bulletSpeed;
    public float bulletDistance;

    private float _elapsedTime;

    void Start()
    {
        _elapsedTime = 0;
    }

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if(_elapsedTime >= cooldown)
        {
            _elapsedTime = 0;
            Fire();
        }
    }

    private void Fire()
    {
        var b = Instantiate(bullet).GetComponent<Bullet>();
        b.Init(transform.position, transform.forward, bulletDistance, bulletSpeed);
    }
}
