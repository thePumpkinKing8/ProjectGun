using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Room[] _rooms; //list of rooms
    [SerializeField] private Room _room; //currently loaded room
    
    // Start is called before the first frame update
    void Start() //loads random room on start
    {
        _room = Instantiate(_rooms[Random.Range(0, _rooms.Length)], new Vector3(0,0,0), Quaternion.identity,  transform.parent = this.gameObject.transform);
    }
    public void SwitchRoom() //switches to a random room in _rooms
    {
        int Rand = Random.Range(0, _rooms.Length);
        _room.Unload();
        _room = Instantiate(_rooms[Rand], new Vector3(0,0,0), Quaternion.identity, transform.parent = this.gameObject.transform);
        
    }
}
