using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    public AudioClip coinGrab;
    public AudioSource audioSource;

	void OnTriggerEnter(Collider c)
    {
        if(c.gameObject.layer == Constantes.LAYER_PLAYER)
        {
            GameManager.instance.AddCoin();
            StartCoroutine(CoinGrab());
        }
    }

    IEnumerator CoinGrab()
    {
        audioSource.PlayOneShot(coinGrab);
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}
