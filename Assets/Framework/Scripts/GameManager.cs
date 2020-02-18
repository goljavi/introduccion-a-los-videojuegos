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
    public int coinsToLife;
    public Vector3 checkPoint;
    public int keys;
    public GameObject KeyPlayer;
    public GameObject[] lifesIcon;
    public Text coinsText;
    public Text lifesNumber;
    public AudioClip keyGrab;
    public AudioClip deathSound;

    AudioSource _as;

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
        _as = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
	}

    private void UpdateInfo()
    {
        if (lifesIcon.Length >= lifes)
        {
            for (var i = 0; i < lifesIcon.Length; i++)
                lifesIcon[i].gameObject.SetActive(false);
            for (var i = 0; i < lifes; i++)
                lifesIcon[i].gameObject.SetActive(true);
        }
        
        coinsText.text = ""+coins+" x";
        lifesNumber.text = "" + lifes;
    }

    public void Die()
    {
        lifes--;
        if (lifes > 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            hasCheckpoint = false;
            lifes = lifesIcon.Length;
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator Respawn()
    {
        Time.timeScale = 0;
        _as.PlayOneShot(deathSound);
        yield return new WaitForSecondsRealtime(0.7f);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void AddCoin()
    {
        coins++;

        if(coinsToLife > 0 && coins >= coinsToLife)
        {
            coins -= coinsToLife;
            lifes++;
        }

        UpdateInfo();
    }

    public void UpdateKeys()
    {
        if (keys > 0 && !KeyPlayer.activeSelf)
        {
            KeyPlayer.SetActive(true);
            _as.PlayOneShot(keyGrab);
        }
        else if (keys == 0 && KeyPlayer.activeSelf)
        {
            KeyPlayer.SetActive(false);
        }
    }

    public void AddLife()
    {
        lifes++;
        UpdateInfo();
    }

    public void NextLevel(string nextLevel)
    {
        hasCheckpoint = false;
       
        if (Application.CanStreamedLevelBeLoaded(nextLevel))
        {
            CameraController.desiredDestination = Vector3.zero;
            SceneManager.LoadScene(nextLevel);
        }        

    }
}
