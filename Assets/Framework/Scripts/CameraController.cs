using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public static Vector3 desiredDestination;

    public float cameraSpeed = 10;

    private Vector3 _desiredPosition;
    private Transform _target;

	void Start () {
        _target = FindObjectOfType<Player>().transform;
        if(desiredDestination == Vector3.zero)
            desiredDestination = transform.position - _target.transform.position;
        _desiredPosition = desiredDestination;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _desiredPosition, cameraSpeed * Time.deltaTime);
    }

    
}
