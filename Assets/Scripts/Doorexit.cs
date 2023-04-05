using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doorexit : MonoBehaviour
{
    private RoomController _roomController;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<RoomController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter(Collider collision)
    {
        _roomController.SwitchRoom();
        Debug.Log("SWITCH");
        
    }
}
