using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorEnter : MonoBehaviour
{
    private Player_Controller _player;
   
    // Start is called before the first frame update
    void Awake()
    {
        _player = GameObject.Find("Player").GetComponent<Player_Controller>();
        
        _player.transform.position = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}