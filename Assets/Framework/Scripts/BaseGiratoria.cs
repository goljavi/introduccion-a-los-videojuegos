using UnityEngine;
using System.Collections;

public class BaseGiratoria : MonoBehaviour {

    public float rotationSpeed;

	void Start () {
	
	}
	
	void Update () {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
	}
}
