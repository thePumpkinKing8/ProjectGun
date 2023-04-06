using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorexit : MonoBehaviour
{
    private RoomController _roomController;
    // Start is called before the first frame update
    void Start()
    {
       _roomController = FindObjectOfType<RoomController>();
      
    }
    void Update() 
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            Debug.Log("SWITCH");
            _roomController.SwitchRoom();
        }
        
       
        
        
    }
    
}
