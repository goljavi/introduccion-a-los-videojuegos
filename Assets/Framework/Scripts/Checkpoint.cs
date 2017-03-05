using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.layer == Constantes.LAYER_PLAYER)
        {
            GameManager.instance.checkPoint = transform.position;
            GameManager.instance.hasCheckpoint = true;
        }
    }
}
