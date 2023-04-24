using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    private Player_Controller _player;
   
    void Awake() //places the player at this location when new room is loaded
    {
        _player = GameObject.Find("Player").GetComponent<Player_Controller>();
        
        _player.transform.position = this.transform.position;
    }

}
