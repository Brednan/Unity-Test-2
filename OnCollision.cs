using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision co)
    {
        if(co.transform.tag == "EnemyBullet")
        {
           Player _player = GetComponent<Player>();
            _player._isDead = true;
            _player._isPaused = false;
        }
    }
}
