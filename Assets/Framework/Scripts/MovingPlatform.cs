using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Vector3 initialPos;
    public Vector3 finalPos;
    public float speed;

    private bool _isGoingInitial;

	void Start () {
        initialPos = new Vector3(initialPos.x, initialPos.y, transform.position.z);
        finalPos = new Vector3(finalPos.x, finalPos.y, transform.position.z);
	}
	
	void Update () {
        if (_isGoingInitial)
        {
            transform.position = Vector3.Lerp(transform.position, initialPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, initialPos) < 0.5f)
                _isGoingInitial = false;
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, finalPos, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, finalPos) < 0.5f)
                _isGoingInitial = true;
        }
	}
}
