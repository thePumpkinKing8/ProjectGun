using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField] private GameObject[] _rooms;
    [SerializeField] private GameObject _room;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchRoom()
    {
        int Rand = Random.Range(0, _rooms.Length);
        Destroy(_room);
        _room = _rooms[Rand];
        Instantiate(_rooms[Rand], new Vector3(0,0,0), Quaternion.identity);
    }
}
