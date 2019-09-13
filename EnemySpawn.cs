using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField]
    GameObject _enemyGameObject;
    Transform _enemyTransform;
    [SerializeField]
    GameObject _playerGameObject;
    Transform _player;
    private float _enemyInstantiateCooldown;
    // Start is called before the first frame update
    void Start()
    {
        _enemyTransform = _enemyGameObject.transform;
        _enemyInstantiateCooldown = Time.time + 10;
        _player = _playerGameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > _enemyInstantiateCooldown)
        {
            EnemyInstantiate();
        }
    }
    private void EnemyInstantiate()
    {
        NavMeshHit hit;
        Vector3 randomPoint = new Vector3(_player.position.x + Random.Range(-30f, 30f), 0, _player.position.z + Random.Range(-30f, 30f));
        NavMesh.SamplePosition(randomPoint, out hit, 100f, NavMesh.AllAreas);
        Vector3 _finalPosition = hit.position;
        Instantiate(_enemyTransform, randomPoint, Quaternion.identity);
        _enemyInstantiateCooldown = Time.time + 10;

    }
}
