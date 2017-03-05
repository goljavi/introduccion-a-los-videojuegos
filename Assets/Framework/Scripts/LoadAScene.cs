using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadAScene : MonoBehaviour {

	public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        if(name=="Level1")
        {
            if (GameManager.instance != null)
            {
                GameManager.instance.lifes = GameManager.instance.lifesIcon.Length;
                GameManager.instance.currentLevel = 1;
                GameManager.instance.coins = 0;
            }
        }
    }
}
