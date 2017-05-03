using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public bool hasCheckpoint;
    public int lifes;
    public int coins;
    public Vector3 checkPoint;
    public int keys;
    public GameObject KeyPlayer;
    public GameObject[] lifesIcon;
    public Text coinsText;

	void Start () {
		if(instance != null)
        {
            lifes = instance.lifes;
            coins = instance.coins;
            checkPoint = instance.checkPoint;
            hasCheckpoint = instance.hasCheckpoint;
            Destroy(instance.gameObject);
        }
        instance = this;
        if (hasCheckpoint)
        {
            FindObjectOfType<Player>().gameObject.transform.position = checkPoint + Vector3.up * 2;
        }
        UpdateInfo();
        FindObjectOfType<Player>().GetComponent<Rigidbody>().velocity = Vector3.zero;
        DontDestroyOnLoad(gameObject);
	}

    private void UpdateInfo()
    {
        if (lifesIcon.Length < lifes) lifes = lifesIcon.Length;
        for (var i = 0; i < lifesIcon.Length; i++)
            lifesIcon[i].gameObject.SetActive(false);
        for (var i = 0; i < lifes; i++)
            lifesIcon[i].gameObject.SetActive(true);

        coinsText.text = ""+coins+" x";
    }

    public void Die()
    {
        lifes--;
        if (lifes > 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            lifes = lifesIcon.Length;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void AddCoin()
    {
        coins++;
        UpdateInfo();
    }

    public void UpdateKeys()
    {
        if (keys > 0 && !KeyPlayer.active)
        {
            KeyPlayer.SetActive(true);
        }
        else if (keys == 0 && KeyPlayer.active)
        {
            KeyPlayer.SetActive(false);
        }
    }

    public void NextLevel(string nextLevel)
    {
        hasCheckpoint = false;
       
        if (Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            SceneManager.LoadScene(nextLevel);
        }        

    }
}
