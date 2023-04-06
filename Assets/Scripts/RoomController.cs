using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private Room[] _rooms;
    [SerializeField] private Room _room;
    
    // Start is called before the first frame update
    void Start()
    {
        _room = Instantiate(_rooms[Random.Range(0, _rooms.Length)], new Vector3(0,0,0), Quaternion.identity,  transform.parent = this.gameObject.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchRoom()
    {
        int Rand = Random.Range(0, _rooms.Length);
        _room.Unload();
        _room = Instantiate(_rooms[Rand], new Vector3(0,0,0), Quaternion.identity, transform.parent = this.gameObject.transform);
        
    }
}
