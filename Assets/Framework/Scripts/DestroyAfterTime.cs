using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

    public float time;

    void Update()
    {
        time -= Time.deltaTime;
        if (time < 0) Destroy(gameObject);
    }
}
