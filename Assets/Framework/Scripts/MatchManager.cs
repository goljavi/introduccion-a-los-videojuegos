using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

    public Text equipoRojo;
    public Text equipoAzul;

    private int _scoreRojo;
    private int _scoreAzul;

    public void AddRedScore()
    {
        _scoreRojo++;
        equipoRojo.text = "EQUIPO ROJO " + _scoreRojo;
    }

    public void AddBlueScore()
    {
        _scoreAzul++;
        equipoAzul.text = "" + _scoreAzul + " EQUIPO AZUL";
    }
}
