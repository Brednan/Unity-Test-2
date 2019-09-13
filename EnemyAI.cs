using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    //Game Objects / Transforms
    private GameObject _player;
    Transform _playerTransform;
    NavMeshAgent _enemy;
    [SerializeField]
    private Transform _bulletSpawnPos;
    [SerializeField]
    private GameObject _bulletPrefab;
    private GameObject _enemyLookat;
    private Transform _enemyLookAtTransform;

    //Floats
    private float _walkSpeed;
    private float _firingCooldown;

    //Vector3's
    Vector3 lookDirection;

    //Animator
    [SerializeField]
    Animator _enemyAnim;

    //Raycast
    RaycastHit hit;
    Ray ray;

    //LayerMask
    [SerializeField]
    LayerMask _enemyLayerMask;

    // Start is called before the first frame update
    void Start()
    {
        _enemyLookat = GameObject.Find("Enemy_LookAt");
        _enemyLookAtTransform = _enemyLookat.transform;
        _firingCooldown = Time.time;
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;

        _walkSpeed = 6.0f;

        _enemy = GetComponent<NavMeshAgent>();
        Physics.IgnoreCollision(_bulletPrefab.GetComponent<Collider>(), GetComponent<Collider>());

    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }
    private void Walk()
    {
        transform.Translate(Vector3.forward * _walkSpeed *Time.deltaTime);
    }
    private void FollowPlayer()
    {
        transform.LookAt(new Vector3(_enemyLookAtTransform.position.x, transform.position.y, _enemyLookAtTransform.position.z));
        if (Vector3.Distance(transform.position, _playerTransform.position) < 6.0f)
        {
            _enemyAnim.Play("Firing_rifle_inPlace");
            if(Time.time > _firingCooldown)
            {
                Shoot();
            }
        }
        else
        {
            Walk();
            _enemyAnim.Play("Rifle_run_inPlace");
        }
    }
    private void Shoot()
    {
        Instantiate(_bulletPrefab, _bulletSpawnPos.position, _bulletSpawnPos.rotation);
        _firingCooldown = Time.time + 0.2f;       
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Bullet")
        {
            Destroy(gameObject);
        }
        if(collision.transform.tag == "EnemyBullet")
        {
            Debug.Log("EnemyBullet");
        }
    }
}
