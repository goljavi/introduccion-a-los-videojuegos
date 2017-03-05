using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float cameraSpeed = 10;

    private Vector3 _desiredPosition;
    private Transform _target;

	void Start () {
        _target = FindObjectOfType<Player>().transform;
        _desiredPosition = transform.position - _target.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = Vector3.Lerp(transform.position, _target.position + _desiredPosition, cameraSpeed * Time.deltaTime);
	}
}
