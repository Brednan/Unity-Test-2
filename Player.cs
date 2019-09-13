using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Game Objects & Transforms
    [SerializeField]
    private Transform cam;
    [SerializeField]
    LayerMask layerMask;
    [SerializeField]
    private GameObject _gun;
    [SerializeField]
    private Transform _bulletSpawn;
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private GameObject _startOver;
    [SerializeField]
    private GameObject _mainMenu;
    [SerializeField]
    private GameObject _crosshair;
    [SerializeField]
    private GameObject _pistol;
    [SerializeField]
    private GameObject _pistol2;
    [SerializeField]
    private GameObject _doublePistols;
    [SerializeField]
    private Transform _pistolBulletSpawn1;
    [SerializeField]
    private Transform _pistolBulletSpawn2;
    [SerializeField]
    private GameObject _pauseUI;
    [SerializeField]
    private GameObject _resumeButton;
    [SerializeField]
    private GameObject _quit;

    //Prefabs
    [SerializeField]
    private Transform _bulletPrefab;

    //Integers
    [SerializeField]
    private int _ammoCount;

    //floats
    private float _shootingCooldown;
    private float _mouseX;
    private float _mouseY;
    private float _walkSpeed;
    Vector3 _gunPosition;
    Quaternion _gunRotation;
    Vector3 _gunScopePosition;
    Quaternion _gunScopeRotation;

    //UI
    [SerializeField]
    GameObject _scopeCrosshair;
    [SerializeField]
    private GameObject _gameOver;


    //RigidBody
    [SerializeField]
    private Rigidbody rb;

    //Bools
    private bool _scopeActive;
    public bool _isPaused;
    public bool _isDead;

    // Start is called before the first frame update
    void Start()
    {

        _doublePistols.SetActive(true);
        _ammoCount = 40;
        _shootingCooldown = Time.time;
        Cursor.lockState = CursorLockMode.Locked;
        _walkSpeed = 10.0f;
        rb.isKinematic = true;
        _gunPosition = new Vector3(0.146f, -0.021f, 0.256f);
        _gunRotation = Quaternion.Euler(-2.586f, -12.639f, -1.122f);
        _gun.transform.localRotation = _gunRotation;
        _gun.transform.localPosition = _gunPosition;
        _camera.fieldOfView = 60f;
        _scopeActive = false;
        _gunScopePosition = new Vector3(-0.413f, 0.048f, 0.131f);
        _gunScopeRotation = Quaternion.Euler(-6.52f, -7.54f, -0.078f);
        _isPaused = false;
        _isDead = false;
        _resumeButton.SetActive(false);
        _quit.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPaused == false && _isDead == false)
        {
            MouseRotation();
            Walk();
            if (_gun.activeSelf == true)
            {

                Shoot();
            }
            else if(_doublePistols.activeSelf == true)
            {
                _firePistols();
            }
        }

        if(_scopeActive == true && _isDead == false)
        {
            _gun.transform.localPosition = _gunScopePosition;
            _gun.transform.localRotation = _gunScopeRotation;
            _camera.fieldOfView = 19.7f;
        }
        else if(_scopeActive == false && _isDead == false)
        {
            _gun.transform.localRotation = _gunRotation;
            _gun.transform.localPosition = _gunPosition;
            _camera.fieldOfView = 60f;
        }


        PlayerDeath();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(_isPaused)
            {
                ResumeGame();
            }
            else
            {
                Pause();
            }
        }
    }
    private void Shoot()
    {
        if (Input.GetMouseButton(0))
        {
            if (_ammoCount > 0)
            {
                if (Time.time > _shootingCooldown)
                { 
                    Instantiate(_bulletPrefab, _bulletSpawn.position, _gun.transform.rotation);
                    _ammoCount -= 1;
                    _shootingCooldown = Time.time + 0.2f;
                    rb.isKinematic = true;
                }
            }
        }
        else if(Input.GetMouseButtonUp(0))
        {
            _ammoCount = 40;
        }
        if (Input.GetMouseButton(1))
        {
            _scopeActive = true;
            _scopeCrosshair.SetActive(true);
        }
        else
        {
            _scopeActive = false;
            _scopeCrosshair.SetActive(false);
        }
    }
    private void MouseRotation()
    {
        _mouseX = Input.GetAxis("Mouse X");
        _mouseY = Input.GetAxis("Mouse Y");

        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y + _mouseX);
        cam.localEulerAngles = new Vector3(cam.localEulerAngles.x - _mouseY, 0);
    }
    private void Walk()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * _walkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * _walkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * _walkSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * _walkSpeed * Time.deltaTime);
        }
    }
    public void Pause()
    {
        if (SceneManager.GetActiveScene().name == "Play")
        {
            _isPaused = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            Debug.Log("pause");
            PauseUI();
        }
    }
    private void PlayerDeath()
    {
        if(_isDead == true)
        {
            if (_isPaused == false)
            {
                _gameOver.SetActive(true);
                _crosshair.SetActive(false);
                _startOver.SetActive(true);
                _scopeActive = false;
                Cursor.lockState = CursorLockMode.None;
                _mainMenu.SetActive(true);
                Time.timeScale = 0;
            }

        }
        if(_isDead == false)
        {

            if(_isPaused == false)
            {
                _gameOver.SetActive(false);
                _crosshair.SetActive(true);
                _startOver.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                _mainMenu.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
    private void _firePistols()
    {
        if (Input.GetMouseButton(0))
        {
            if(_ammoCount > 0)
            {
                if(Time.time > _shootingCooldown)
                {
                    Instantiate(_bulletPrefab, _pistolBulletSpawn1.position, _pistol.transform.rotation);
                    Instantiate(_bulletPrefab, _pistolBulletSpawn2.position, _pistol2.transform.rotation);
                    _ammoCount -= 1;
                    _shootingCooldown = Time.time + 0.2f;
                }
            }

        }
    }
    private void PauseUI()
    {
        _startOver.SetActive(true);
        _mainMenu.SetActive(true);
        _resumeButton.SetActive(true);
        _quit.SetActive(true);
    }
    public void ResumeGame()
    {
        Debug.Log("unpause");
        _startOver.SetActive(false);
        _mainMenu.SetActive(false);
        _crosshair.SetActive(true);
        _scopeActive = false;
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        _resumeButton.SetActive(false);
        _quit.SetActive(false);
        _isPaused = false;
    }
}


