using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trampa : MonoBehaviour {

	void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == Constantes.LAYER_PLAYER)
            GameManager.instance.Die();
    }
}
