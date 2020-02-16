using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

    public Vector3 initialPos;
    public Vector3 finalPos;
    public float speed;

    private bool _isGoingInitial;

	void Start () {
        initialPos = transform.position;
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

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.transform.GetComponent<Player>();
        if (player && player.transform.position.y > transform.position.y + .5f)
            player.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        var player = collision.transform.GetComponent<Player>();
        if (player)
            player.transform.SetParent(null);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(finalPos, .25f);
    }
}
