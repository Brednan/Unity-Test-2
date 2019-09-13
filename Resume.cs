using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    [SerializeField]
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResumeFunction()
    {
        _player.ResumeGame();
    }
    public void PauseFunction()
    {
        _player.Pause();
    }
    public void Quit()
    {
        Application.Quit();
    }
}
