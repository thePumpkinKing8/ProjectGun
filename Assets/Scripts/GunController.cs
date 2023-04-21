using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private int _activeGun;
    [SerializeField] private GameObject _socket;
    [SerializeField] private Gun[] _guns; //make sure the objects in this list are from the scene and not the prefab folder
    // Start is called before the first frame update
    void Start() //deactivates all guns then activates the first gun in the list
    {
        foreach (Gun gun in _guns)
        {
            gun.Deactivate();
        }
        _guns[0].Activate();
        _activeGun = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)) //deactivates the first gun then activates the next gun in the list 
        {
            if(_activeGun >= _guns.Length -1) //wraps the list once the end is reached
            {
                _guns[_activeGun].Deactivate();
                _activeGun = 0;
                _guns[_activeGun].Activate();
            }
            else
            {
                _guns[_activeGun].Deactivate();
                _activeGun++;
                _guns[_activeGun].Activate();
            }
        }
        _guns[_activeGun].transform.position = _socket.transform.position;
    }
}
