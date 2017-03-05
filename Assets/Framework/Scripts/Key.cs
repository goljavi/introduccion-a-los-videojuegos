using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == Constantes.LAYER_PLAYER)
        {
            GameManager.instance.keys++;
            GameManager.instance.UpdateKeys();
            Destroy(gameObject);
        }
    }
}
