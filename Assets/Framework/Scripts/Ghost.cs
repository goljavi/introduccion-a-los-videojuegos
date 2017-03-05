using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour {

    public float moveDistance;
    public float speed;
    
    private float _currentDistance;

	// Use this for initialization
	void Start () {
        _currentDistance = 0;	
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.forward * speed * Time.deltaTime;
        _currentDistance += Time.deltaTime * speed;
        if (moveDistance < _currentDistance)
        {
            _currentDistance = 0;
            transform.forward = -transform.forward;
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == Constantes.LAYER_PLAYER)
            GameManager.instance.Die();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * moveDistance);
    }
}
