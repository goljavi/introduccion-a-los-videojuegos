using UnityEngine;
using System.Collections;

public class JoystickVibrationManager : MonoBehaviour {

    private float[] _vibrationTime;
    private float[] _vibrationAmmount;
    private bool _isEnabled = true;

    /// <summary>
    /// Crea los valores iniciales para los vibradores
    /// </summary>
	void Start () {
        _vibrationAmmount = new float[4];
        _vibrationTime = new float[4];
	}
	
    /// <summary>
    /// Actualiza la vibración de todos los joysticks
    /// </summary>
	void Update () {
       /* int length = _vibrationAmmount.Length;
        for(int i = 0; i < length; i++)
        {
            PlayerIndex pi;
            switch(i)
            {
                case 0:
                    pi = PlayerIndex.One;
                    break;
                case 1:
                    pi = PlayerIndex.Two;
                    break;
                case 2:
                    pi = PlayerIndex.Three;
                    break;
                default:
                    pi = PlayerIndex.Four;
                    break;
            }
            if(_vibrationTime[i]>0)
            {
                _vibrationTime[i] -= Time.deltaTime;
                if (_vibrationTime[i] <= 0)
                    _vibrationAmmount[i] = 0;
                GamePad.SetVibration(pi, _vibrationAmmount[i], _vibrationAmmount[i]);
            }
        }*/
	}

    /// <summary>
    /// Crea una nueva vibración al jugador 1
    /// </summary>
    /// <param name="timeAmmount">Tiempo de vibración</param>
    /// <param name="vibrationAmmount">Cantidad de vibración (entre 0 y 1)</param>
    public void CreateNewVibration(float timeAmmount, float vibrationAmmount)
    {
        if (_isEnabled)
        {
            if (vibrationAmmount > 1)
                vibrationAmmount = 1;
            else if (vibrationAmmount < 0)
                vibrationAmmount = 0;
            _vibrationAmmount[0] = vibrationAmmount;
            _vibrationTime[0] = timeAmmount;
        }
    }

    /// <summary>
    /// Crea una nueva vibración
    /// </summary>
    /// <param name="timeAmmount">Tiempo de vibración</param>
    /// <param name="vibrationAmmount">Cantidad de vibración (entre 0 y 1)</param>
    /// <param name="playerIndex">Jugador que recibirá la vibración (entre 0 y 3)</param>
    public void CreateNewVibration(float timeAmmount, float vibrationAmmount, int playerIndex)
    {
        if (vibrationAmmount > 1)
            vibrationAmmount = 1;
        else if (vibrationAmmount < 0)
            vibrationAmmount = 0;
        if (playerIndex > 3)
            playerIndex = 3;
        else if (playerIndex < 0)
            playerIndex = 0;
        _vibrationAmmount[playerIndex] = vibrationAmmount;
        _vibrationTime[playerIndex] = timeAmmount;
    }

    /// <summary>
    /// Agrega una vibración a todos los joysticks.
    /// </summary>
    /// <param name="timeAmmount">Duración.</param>
    /// <param name="vibrationAmmount">Cantidad de vibración.</param>
    public void CreateEveryoneNewVibration(float timeAmmount, float vibrationAmmount)
    {
        if (_isEnabled)
        {
            int ammount = _vibrationAmmount.Length;
            for (int i = 0; i < ammount; i++)
            {
                _vibrationAmmount[i] = vibrationAmmount;
                _vibrationTime[i] = timeAmmount;
            }
        }
    }

    /// <summary>
    /// Para la vibración a un jugador
    /// </summary>
    /// <param name="playerIndex">Jugador que dejará de vibrar (entre 0 y 3)</param>
    public void StopVibration(int playerIndex)
    {
        if (playerIndex > 3)
            playerIndex = 3;
        else if (playerIndex < 0)
            playerIndex = 0;
        _vibrationTime[playerIndex] = 0;
        /*PlayerIndex pi;
        switch (playerIndex)
        {
            case 0:
                pi = PlayerIndex.One;
                break;
            case 1:
                pi = PlayerIndex.Two;
                break;
            case 2:
                pi = PlayerIndex.Three;
                break;
            default:
                pi = PlayerIndex.Four;
                break;
        }
        GamePad.SetVibration(pi, 0, 0);*/
    }

    /// <summary>
    /// Activa o desactiva el rumble para todos
    /// </summary>
    /// <param name="status">Activar o desactivar</param>
    public void EnableOrDisableVibration(bool status)
    {
        _isEnabled = status;
    }
}
