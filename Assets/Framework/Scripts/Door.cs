using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private Vector3 _openPosition;
    private bool _open;

	// Use this for initialization
	void Start () {
        _openPosition = transform.position + Vector3.up * 5;
	}
	
	// Update is called once per frame
	void Update () {
        if (_open)
            transform.position = Vector3.Lerp(transform.position, _openPosition, 5 * Time.deltaTime);
	}

    void OnTriggerEnter(Collider c)
    {
        if (!_open)
        {
            if (c.gameObject.layer == Constantes.LAYER_PLAYER)
            {
                if (GameManager.instance.keys > 0)
                {
                    GameManager.instance.keys--;
                    GameManager.instance.UpdateKeys();
                    _open = true;
                }
            }
        }
    }
}
