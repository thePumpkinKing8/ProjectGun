using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player_Controller _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponentInChildren<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void NextRoom(Vector3 Position)
    {
        _player.SetPosition(Position);
    }
    
}
