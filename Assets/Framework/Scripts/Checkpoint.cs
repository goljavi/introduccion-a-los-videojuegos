﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
    bool activated;

	void OnTriggerEnter(Collider c)
    {
        if (!activated && c.gameObject.layer == Constantes.LAYER_PLAYER)
        {
            GameManager.instance.checkPoint = transform.position;
            GameManager.instance.hasCheckpoint = true;
            transform.GetChild(0).GetComponent<MeshRenderer>().materials[1].SetFloat("_RimPower", 0.1f);
            activated = true;
        }
    }
}
