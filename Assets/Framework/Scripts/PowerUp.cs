using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public enum PowerUpType
    {
        morePull,
        Faster,
        MoreJump
    }

    public PowerUpType type;
    public float morePull;
    public float speed;
    public float moreJump;
    public float cdToRespawn;
    public float bonusDuration;

    private float _currentCdToRespawn;
    private MeshRenderer _meshRenderer;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _currentCdToRespawn = cdToRespawn;
    }

    void Update()
    {
        _currentCdToRespawn+= Time.deltaTime;
        if (_currentCdToRespawn < cdToRespawn) _meshRenderer.enabled = false;
        else _meshRenderer.enabled = true;
    }

    public bool CanUse
    {
        get
        {
            if (_currentCdToRespawn >= cdToRespawn)
            {
                _currentCdToRespawn = 0;
                return true;
            }
            else
                return false;
        }
    }
}
