using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float _distanceLeft = 99999999;
    private float _speed;

	void Update () {
        transform.position += transform.forward * _speed * Time.deltaTime;
        _distanceLeft -= _speed * Time.deltaTime;
        if (_distanceLeft < 0)
            Destroy(gameObject);
	}

    public void Init(Vector3 position, Vector3 foward, float distance, float speed)
    {
        _distanceLeft = distance;
        _speed = speed;
        transform.position = position;
        transform.forward = foward;
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == Constantes.LAYER_PLAYER)
            GameManager.instance.Die();
    }
}
