using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlataform : MonoBehaviour {

    public float force;

	void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == Constantes.LAYER_PLAYER)
        {
            c.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
            c.gameObject.GetComponent<Rigidbody>().AddForce(force * transform.forward, ForceMode.VelocityChange);
            //c.gameObject.GetComponent<Rigidbody>().AddRelativeForce(force * transform.forward, ForceMode.VelocityChange);
        }
    }
}
