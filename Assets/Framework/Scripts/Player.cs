using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player : MonoBehaviour {

    public int player;
    public float jumpPower;
    public float movSpeed;
    public float cdToHit;
    public float explosionForce;
    public float explosionRadius;
    public bool canDoubleJump;
    public Animator animator;
    public bool enableRumble;
    public bool enableSounds;
    public AudioClip jump;
    public AudioClip hit;
    public AudioClip die;
    public AudioClip step;
    public AudioClip powerUp;
    public AudioClip music;
    //public Transform spawnGroundParticles;
    public bool enableParticles;
    public GameObject stepParticlePrefab;
    //public GameObject jumpParticlePrefab;
    
    private TrailRenderer _trailRenderer;
    private PostprocessTemplate _postProcess;
    private SoundManager _soundManager;
    private float _currentBonusJump;
    private float _currentBonusSpeed;
    private float _currentBonusPull;
    private float _powerUpTime;
    private bool _canJump;
    private bool _doubleJump;
    private Rigidbody _rigidBody;
    private float _currentCDToHit;
    private Rigidbody _enemyRigidBody;
    private Vector3 _spawnPoint;
    private bool _falling;
    private bool _jumpButton;
    private bool _releasedJumpButton;
    private float _delayToJumpOver;
    private float _joystickVibration;

    private const string ANIM_JUMP = "Jump";
    private const string ANIM_SHIELDATTACK = "Shield Attack";
    private const string ANIM_IDLE = "Idle";
    private const string ANIM_STUN = "Stun";
    private const string ANIM_KNOCKBACK = "Knockback";
    private const string ANIM_RUN = "Run";
    private const string ANIM_FALL = "Fall";
    private const string ANIM_STARTRUN = "StartRun";

	void Start () {

        _postProcess = FindObjectOfType<PostprocessTemplate>();

        _trailRenderer = GetComponent<TrailRenderer>();
        _trailRenderer.enabled = false;

        _soundManager = FindObjectOfType<SoundManager>();
        _soundManager.SetSFXVolume(0.3f);
        
        _falling = false;
        _spawnPoint = transform.position;
        _canJump = true;
        if (canDoubleJump) _doubleJump = true;
        else _doubleJump = false;
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.velocity = Vector3.zero;

        var players = FindObjectsOfType<Player>();
        for (int i = players.Length - 1; i >= 0; i--)
        {
            if (players[i] != this)
                _enemyRigidBody = players[i].GetComponent<Rigidbody>();
        }
        _joystickVibration = 0;
        _currentCDToHit = cdToHit;

        if (player == 1 && enableSounds)
        {
            _soundManager.SetMasterVolume(0.5f);
            _soundManager.SetMusicVolume(5);
            _soundManager.ReproduceSound(SoundManager.SoundType.Music, music);
        }
	}

    void Update() {
        if (enableRumble)
        {
            PlayerIndex pi;
            if (player == 1) pi = PlayerIndex.One;
            else pi = PlayerIndex.Two;
            GamePad.SetVibration(pi, _joystickVibration, _joystickVibration);
        }
        _joystickVibration -= _joystickVibration * Time.deltaTime + 0.5f * Time.deltaTime;
        if (_joystickVibration < 0) _joystickVibration = 0;
        _delayToJumpOver -= Time.deltaTime;
        _currentCDToHit += Time.deltaTime;
        _powerUpTime -= Time.deltaTime;
        if(_powerUpTime <= 0)
        {
            _trailRenderer.enabled = false;
            _currentBonusJump = 0;
            _currentBonusPull = 0;
            _currentBonusSpeed = 0;
        }
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || XboxCtrlrInput.XCI.GetButtonDown(XboxCtrlrInput.XboxButton.A, player)) && _canJump)
        {
            _jumpButton = false;
            if (_doubleJump)
            {
                if(enableSounds)
                    _soundManager.ReproduceSound(SoundManager.SoundType.SFX, jump);
                _joystickVibration = 0.3f;
                _doubleJump = false;

                _rigidBody.AddForce(Vector3.up * (jumpPower + _currentBonusJump), ForceMode.VelocityChange);
            }
            else
            {
                if(enableSounds)
                    _soundManager.ReproduceSound(SoundManager.SoundType.SFX, jump);
                _joystickVibration = 0.2f;
                _canJump = false;
               
                if (canDoubleJump && (Vector3.Angle(_rigidBody.GetPointVelocity(transform.position), Vector3.up) < 90))
                    _rigidBody.AddForce(Vector3.up * (jumpPower + _currentBonusJump) / 1.5f, ForceMode.Impulse);
                else if(canDoubleJump)
                    _rigidBody.AddForce(Vector3.up * (jumpPower + _currentBonusJump) * 1.5f, ForceMode.Impulse);
                else
                    _rigidBody.AddForce(Vector3.up * (jumpPower + _currentBonusJump), ForceMode.Impulse);
            }
            animator.SetTrigger(ANIM_JUMP);
        }
        bool walking = false;

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || XboxCtrlrInput.XCI.GetAxisRaw(XboxCtrlrInput.XboxAxis.LeftStickX, player) < -0.2f)
        {
            walking = true;
            _rigidBody.AddForce(Vector3.left * (movSpeed + _currentBonusSpeed));
            transform.forward = -Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || XboxCtrlrInput.XCI.GetAxisRaw(XboxCtrlrInput.XboxAxis.LeftStickX, player) > 0.2f)
        {
            walking = true;
            transform.forward = Vector3.forward;
            _rigidBody.AddForce(-Vector3.left * (movSpeed + _currentBonusSpeed));
        }
        /*if (_currentCDToHit >= cdToHit && (Input.GetKeyDown(KeyCode.S) || XboxCtrlrInput.XCI.GetButtonDown(XboxCtrlrInput.XboxButton.X, player)))
        {
            if (enableSounds)
                _soundManager.ReproduceSound(SoundManager.SoundType.SFX, hit);
            _joystickVibration = 0.5f;
            animator.SetTrigger(ANIM_SHIELDATTACK);
            _currentCDToHit = 0;
            if (Vector3.Distance(transform.position, _enemyRigidBody.transform.position) > Vector3.Distance(transform.position + transform.right / 5, _enemyRigidBody.transform.position) && Mathf.Abs(transform.position.y - _enemyRigidBody.transform.position.y) < 1)
            {
                if(Vector3.Distance(transform.position, _enemyRigidBody.transform.position) < 2f)
                    _postProcess.noiseAmmount = 0.03f;
                _enemyRigidBody.GetComponent<Player>().AddVibration(0.6f);
                var v = new Vector3(_enemyRigidBody.transform.position.x - transform.position.x, 0, 0);
                _enemyRigidBody.AddExplosionForce((explosionForce + _currentBonusPull), _enemyRigidBody.transform.position - v, explosionRadius);
            }
        }*/

        animator.SetBool(ANIM_IDLE, !walking);
        animator.SetBool(ANIM_RUN, walking);
	}

    public void AddVibration(float ammount)
    {
        //_joystickVibration += ammount;
    }

    public void Step()
    {
        if (enableParticles && ((_doubleJump && canDoubleJump) || (_canJump && !canDoubleJump)))
        {

            if (enableSounds)
                _soundManager.ReproduceSound(SoundManager.SoundType.SFX, step);
        }
    }

    void OnTriggerExit(Collider c)
    {
        switch(c.gameObject.layer)
        {
            case Constantes.LAYER_BASE:
                animator.SetBool(ANIM_IDLE, false);
                _falling = true;
                break;
        }
    }

    void OnTriggerEnter(Collider c)
    {
        switch(c.gameObject.layer)
        {
            case Constantes.LAYER_BASE:
                _canJump = true;
                _falling = false;
                animator.SetBool(ANIM_IDLE, true);
                if (canDoubleJump) _doubleJump = true;
                break;
            case Constantes.LAYER_POWERUP:
                PowerUp pu = c.gameObject.GetComponent<PowerUp>();
                if(pu.CanUse)
                {
                    _trailRenderer.Clear();
                    _trailRenderer.enabled = true;
                    _soundManager.ReproduceSound(SoundManager.SoundType.SFX, powerUp);
                    switch(pu.type)
                    {
                        case PowerUp.PowerUpType.Faster:
                            _currentBonusSpeed = pu.speed;
                            break;
                        case PowerUp.PowerUpType.MoreJump:
                            _currentBonusJump = pu.moreJump;
                            break;
                        case PowerUp.PowerUpType.morePull:
                            _currentBonusPull = pu.morePull;
                            break;
                    }
                    _powerUpTime = pu.bonusDuration;
                }
                break;
            
            case Constantes.LAYER_PLAYER:
                _canJump = true;
                _falling = false;
                if (canDoubleJump) _doubleJump = true;
                break;
            case Constantes.LAYER_AUTOJUMP:
                if (_delayToJumpOver < 0)
                {
                    _delayToJumpOver = 0.2f;
                    if (canDoubleJump && (Vector3.Angle(_rigidBody.GetPointVelocity(transform.position), Vector3.up) < 90))
                        _rigidBody.AddForce(Vector3.up * (jumpPower + _currentBonusJump) / 1.5f, ForceMode.Impulse);
                    else if (canDoubleJump)
                        _rigidBody.AddForce(Vector3.up * (jumpPower + _currentBonusJump) * 1.5f, ForceMode.Impulse);
                    else
                        _rigidBody.AddForce(Vector3.up * (jumpPower + _currentBonusJump), ForceMode.Impulse);
                    animator.SetTrigger(ANIM_JUMP);
                }
                break;
        }
    }

    public void RePosicionar()
    {
        _rigidBody.isKinematic = true;
        transform.position = _spawnPoint;
        _rigidBody.isKinematic = false;
        _delayToJumpOver = 0.5f;
        _powerUpTime = 0;
    }
}
