using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpVida : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == Constantes.LAYER_PLAYER)
        {
            GameManager.instance.AddLife();
            Destroy(gameObject);
        }
    }
}
