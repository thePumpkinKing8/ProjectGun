using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Player_Controller _player;

    public Player_Controller Player
    {
        get => _player;
    }

    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponentInChildren<Player_Controller>();
    }

}
